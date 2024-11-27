using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Telemedicine.BusinessLogic;
using Telemedicine.BusinessLogic.Interfaces;
using Telemedicine.Domain.Entities.DocData;
using Telemedicine.Domain.Entities.UserData;
using TelemedicineApi.Extensions;
using TelemedicineApi.Models;

namespace TelemedicineApi.Controllers
{
    public class DoctorController : ApiController
    {
        private readonly IMainApp _mainAppBusinessLogic;

        public DoctorController()
        {
            var businessLogic = new GlobalBusinessLogic();
            _mainAppBusinessLogic = businessLogic.GetMainAppBL();
        }

        [HttpGet]
        public IHttpActionResult GetDoctorList()
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.Contains("token"))
            {
                string token = headers.GetValues("token").First();
                var vToken = _mainAppBusinessLogic.ValidateSession(token);
                if (vToken)
                {
                    var doclist = _mainAppBusinessLogic.GetDocsList(HttpContext.Current.Server.MapPath("~/"));
                    return Ok(doclist);
                }
                return Content(HttpStatusCode.Unauthorized, "ERROR");
            }

            return Content(HttpStatusCode.NotAcceptable, "ERROR");
        }

        [Route("api/Doctor/GetDoctor/{id}")]
        public IHttpActionResult GetDoctor(int id)
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.Contains("token"))
            {
                string token = headers.GetValues("token").First();
                var vToken = _mainAppBusinessLogic.ValidateSession(token);
                if (vToken)
                {
                    var path = HttpContext.Current.Server.MapPath("~/");
                    var doc = _mainAppBusinessLogic.GetDoc(path, id);

                    return Ok(doc);
                }
                return Content(HttpStatusCode.Unauthorized, "ERROR");
            }

            return Content(HttpStatusCode.NotAcceptable, "ERROR");
        }

        [HttpPost]
        [SanitizeInput]
        public IHttpActionResult AddConsultation([FromBody]UserConsult consdata)
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.Contains("token"))
            {
                string token = headers.GetValues("token").First();
                var vToken = _mainAppBusinessLogic.ValidateSession(token);
                if (vToken)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<UserConsult, DocConsult>();
                    });

                    var consultation = Mapper.Map<DocConsult>(consdata);
                    var data = _mainAppBusinessLogic.AddConsultation(consultation);
                    if (data != null)
                    {
                        return Ok(data);
                    }
                }
                return Content(HttpStatusCode.Unauthorized, "ERROR");
            }

            return Content(HttpStatusCode.NotAcceptable, "ERROR");
        }
    }
}
