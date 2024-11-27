using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Telemedicine.BusinessLogic;
using Telemedicine.BusinessLogic.Interfaces;
using Telemedicine.Domain.Entities.UserData;
using TelemedicineApi.Models;

namespace TelemedicineApi.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IMainApp _mainAppBusinessLogic;
        public ProfileController()
        {
            var businessLogic = new GlobalBusinessLogic();
            _mainAppBusinessLogic = businessLogic.GetMainAppBL();
        }

        [HttpGet]
        public IHttpActionResult GetProfile()
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.Contains("token"))
            {
                string token = headers.GetValues("token").First();

                var action = new UserOnAction { Token = token };
                var profile = _mainAppBusinessLogic.GetUserProfile(action);
                if (profile.Status != null)
                {
                    return Content(HttpStatusCode.ExpectationFailed, profile.Status);
                }

                return Ok(profile);
            }

            return Content(HttpStatusCode.NotAcceptable, "ERROR");
        }

        [HttpPost]
        public IHttpActionResult UpdateProfile([FromBody] UserUpdateProfile data)
        {
            return Content(HttpStatusCode.NotImplemented, "ERROR");
        }
    }
}
