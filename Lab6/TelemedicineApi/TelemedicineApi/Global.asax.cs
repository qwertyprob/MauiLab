using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Telemedicine.BusinessLogic.DataBaseModel.InitialSeed;

namespace TelemedicineApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           BundleConfig.RegisterBundles(BundleTable.Bundles);


            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new DoctorContextInitializer());
        }
    }
}
