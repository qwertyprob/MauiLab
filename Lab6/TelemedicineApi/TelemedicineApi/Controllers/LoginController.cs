using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Telemedicine.BusinessLogic;
using Telemedicine.BusinessLogic.Interfaces;
using Telemedicine.Domain.Entities.UserData;
using TelemedicineApi.Extensions;
using TelemedicineApi.Models;

namespace TelemedicineApi.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IMainApp _mainAppBusinessLogic;
        public LoginController()
        {
            var businessLogic = new GlobalBusinessLogic();
            _mainAppBusinessLogic = businessLogic.GetMainAppBL();
        }

        [HttpPost]
        [SanitizeInput]
        public IHttpActionResult UserAuth(UserCredential data)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserCredential, UserLogin>();
            });

            var credential = Mapper.Map<UserLogin>(data);
            credential.LoginIp = GetClientIp();
            credential.LoginDateTime = DateTime.Now;

            var status = _mainAppBusinessLogic.UserLogIn(credential);
            return Content(status.Status != "SUCCESS" ? HttpStatusCode.ExpectationFailed : HttpStatusCode.OK, status);
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
