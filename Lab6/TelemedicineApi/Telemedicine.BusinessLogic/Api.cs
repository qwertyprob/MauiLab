using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Telemedicine.BusinessLogic.DataBaseModel;
using Telemedicine.BusinessLogic.Helpers;
using Telemedicine.Domain.Collection;
using Telemedicine.Domain.Entities.DocData;
using Telemedicine.Domain.Entities.Responses;
using Telemedicine.Domain.Entities.Session;
using Telemedicine.Domain.Entities.UserData;

namespace Telemedicine.BusinessLogic
{
    class Api
    {
        protected ResponseMessage RegUserAction(UserProfile profile)
        {
            var validate = new EmailAddressAttribute();
            if (!validate.IsValid(profile.Email))
            {
                return new ResponseMessage
                { Status = "ERROR", Message = "INVALID EMAIL" };
            }

            try
            {
                profile.Password = SGlobalLogic.HashGen(profile.Password);
                profile.Photo = SGlobalLogic.ConvertImage(profile.Base64Photo, profile.ServerPath);

                try
                {
                    using (var db = new UserContext())
                    {
                        var vEmail = db.Users.Count(local => local.Email == profile.Email);
                        if (vEmail != 0)
                        {
                            return new ResponseMessage
                            { Status = "ERROR", Message = "THE USER WITH THIS EMAIL EXIST" };
                        }

                        db.Users.Add(profile);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return new ResponseMessage
                    { Status = "ERROR", Message = e.Message };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage
                { Status = "ERROR", Message = e.Message };
            }

            return null;
        }
        protected ResponseMessage UserLoginAction(UserLogin credential)
        {
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(credential.Email))
            {
                UserProfile user;
                var pass = SGlobalLogic.HashGen(credential.Password);
                using (var db = new UserContext())
                {
                    var vCredential = db.Users.FirstOrDefault(u => u.Email == credential.Email && u.Password == pass);
                    if (vCredential == null)
                    {
                        return new ResponseMessage
                        { Status = "ERROR", Message = "INVALID EMAIL OR PASSWORD" };
                    }

                    vCredential.LasIp = credential.LoginIp;
                    vCredential.LastLogin = credential.LoginDateTime;

                    using (var todo = new UserContext())
                    {
                        todo.Entry(vCredential).State = EntityState.Modified;
                        todo.SaveChanges();
                    }

                    user = vCredential;
                }

                var token = GenToken(user.Username);

                return new ResponseMessage
                { Status = "SUCCESS", Message = token };
            }
            else
            {
                return new ResponseMessage
                { Status = "ERROR", Message = "INVALID EMAIL" };
            }
        }
        private string GenToken(string username)
        {
            var token = SGlobalLogic.TokenGenerator(28);

            using (var db = new SessionContext())
            {
                var curent = (from e in db.Tokens where e.Username == username select e).FirstOrDefault();
                if (curent != null)
                {
                    curent.TokenString = token;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Tokens.Add(new Token { Username = username, TokenString = token, ExpireTime = DateTime.Now.AddMinutes(60) });
                    db.SaveChanges();
                }
            }

            return token;
        }
        protected UserMinimalData UserProfileAction(UserOnAction action)
        {
            string username;
            try
            {
                if (!string.IsNullOrEmpty(action.Token))
                {
                    using (var db = new SessionContext())
                    {
                        var session = db.Tokens.FirstOrDefault(t => t.TokenString == action.Token);
                        if (session != null)
                        {
                            username = session.Username;
                        }
                        else
                        {
                            return new UserMinimalData
                            {
                                Status = new ResponseMessage
                                {
                                    Message = "ACTION EXPECTED",
                                    Status = "ERROR"
                                }
                            };
                        }
                    }

                    using (var db = new UserContext())
                    {
                        var profile = db.Users.FirstOrDefault(u => u.Username == username);
                        if (profile != null)
                        {
                            Mapper.Initialize(cfg =>
                            {
                                cfg.CreateMap<UserProfile, UserMinimalData>();
                            });

                            var minimal = Mapper.Map<UserMinimalData>(profile);
                            minimal.Base64Photo = SGlobalLogic.GenerateImage(profile.Photo);
                            return minimal;
                        }

                        return new UserMinimalData
                        {
                            Status = new ResponseMessage
                            {
                                Message = "ACTION EXPECTED",
                                Status = "ERROR"
                            }
                        };
                    }
                }

                return new UserMinimalData
                {
                    Status = new ResponseMessage
                    {
                        Message = "TOKEN EXPECTED",
                        Status = "ERROR"
                    }
                };
            }
            catch (Exception e)
            {
                return new UserMinimalData
                {
                    Status = new ResponseMessage
                    {
                        Message = e.Message,
                        Status = "ERROR"
                    }
                };
            }
        }
        protected IEnumerable<DocMinimalProfile> DoctorList(string path)
        {
            IEnumerable<DocProfile> docProfile;
            using (var db = new DoctorContext())
            {
                docProfile = db.Docs.Where(d => d.IsActive).ToList();
                if (!docProfile.Any())
                {
                    return new DocMinimalProfile[0];
                }
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DocProfile, DocMinimalProfile>();
            });

            var docMinimalProfile = Mapper.Map<List<DocMinimalProfile>>(docProfile);
            foreach (var profile in docMinimalProfile)
            {
                profile.Photo = SGlobalLogic.GenerateImage(path + profile.Photo);
            }

            return docMinimalProfile;
        }
        protected DocMinimalProfile DetDocProfile(string path, int id)
        {
            DocProfile docProfile;
            using (var db = new DoctorContext())
            {
                docProfile = db.Docs.FirstOrDefault(d => d.IsActive && d.DocId == id);
                if (docProfile == null)
                {
                    return new DocMinimalProfile();
                }
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DocProfile, DocMinimalProfile>();
            });

            var docMinimalProfile = Mapper.Map<DocMinimalProfile>(docProfile);
            docMinimalProfile.Photo = SGlobalLogic.GenerateImage(path + docMinimalProfile.Photo);

            return docMinimalProfile;
        }
        protected DocConsultMinimal AddConsToDoc(DocConsult consultation)
        {
            Random rnd = new Random();

            var specs = Disease.FindDocByDisease(consultation.Disease);
            if (specs != "N/A")
            {
                try
                {
                    using (var db = new DoctorContext())
                    {
                        var doc = new List<DocProfile>();
                        foreach (var profile in db.Docs.Where(d => d.Specs == specs))
                            doc.Add(profile);
                        if (doc.Count > 0)
                        {
                            var r = rnd.Next(doc.Count);
                            consultation.DocId = doc[r].DocId;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }

                try
                {
                    using (var db = new ConsultationContext())
                    {
                        db.Cons.Add(consultation);
                        db.SaveChanges();
                    }

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DocConsult, DocConsultMinimal>();
                    });

                   return Mapper.Map<DocConsultMinimal>(consultation);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
