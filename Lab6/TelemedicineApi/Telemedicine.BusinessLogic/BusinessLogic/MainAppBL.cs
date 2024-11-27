using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.BusinessLogic.Helpers;
using Telemedicine.BusinessLogic.Interfaces;
using Telemedicine.Domain.Entities.DocData;
using Telemedicine.Domain.Entities.Responses;
using Telemedicine.Domain.Entities.UserData;

namespace Telemedicine.BusinessLogic.BusinessLogic
{
    class MainAppBL : Api, IMainApp
    {

        public bool ValidateSession(string token)
        {
            return SGlobalLogic.TokenValidator(token);
        }

        public ResponseMessage RegNewUser(UserProfile profile)
       {
           return RegUserAction(profile);
       }

        public ResponseMessage UserLogIn(UserLogin data)
        {
            return UserLoginAction(data);
        }

        public UserMinimalData GetUserProfile(UserOnAction action)
        {
            return UserProfileAction(action);
        }

        public IEnumerable<DocMinimalProfile> GetDocsList(string path)
        {
            return DoctorList(path);
        }

        public DocMinimalProfile GetDoc(string path, int id)
        {
            return DetDocProfile(path, id);
        }

        public DocConsultMinimal AddConsultation(DocConsult consultation)
        {
            return AddConsToDoc(consultation);
        }
    }
}
