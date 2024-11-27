using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Telemedicine.BusinessLogic;
using Telemedicine.BusinessLogic.Interfaces;
using Telemedicine.Domain.Entities.UserData;
using TelemedicineApi.Extensions;
using TelemedicineApi.Models;

namespace TelemedicineApi.Controllers
{
    public class RegisterController : ApiController
    {
        private readonly IMainApp _mainAppBusinessLogic;

        public RegisterController()
        {
            var businessLogic = new GlobalBusinessLogic();
            _mainAppBusinessLogic = businessLogic.GetMainAppBL();
        }

        [HttpPost]
        [SanitizeInput]
        public IHttpActionResult UserReg([FromBody] UserRegistration data)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserRegistration, UserProfile>();
            });

            var profile = Mapper.Map<UserProfile>(data);
            profile.ServerPath = HttpContext.Current.Server.MapPath("~/");
            profile.LasIp = GetClientIp();
            profile.RegDate = DateTime.Now;
            profile.LastLogin = DateTime.Now;

            var status = _mainAppBusinessLogic.RegNewUser(profile);
            if (status != null)
            {
                return Content(HttpStatusCode.ExpectationFailed, status);
            }
            else
            {
                return Content(HttpStatusCode.Created, "SUCCESS");
            }
        }


        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
}
