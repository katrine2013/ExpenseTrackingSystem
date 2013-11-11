using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;

namespace ExpenseTrackingSystem.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Logger log;

        protected void Application_Start()
        {
            try
            {
                log = LogManager.GetCurrentClassLogger();

                AreaRegistration.RegisterAllAreas();

                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);

                log.Trace("Application was started");

            }
            catch (Exception e)
            {
                log.ErrorException("Exeption during start application: " + e.Message, e);
            }
          
        }
    }
}