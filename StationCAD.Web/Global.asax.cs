using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using NLog;

namespace StationCAD.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError().GetBaseException();

                if (ex.GetType() == typeof(HttpException) && ((HttpException)ex).GetHttpCode() == 404)
                {
                    // 404 errors are handled by the application. No requirement to log
                    return;
                }

                logger.Error(ex, "StationCAD Application Exception. Message: {0}", ex.Message);
            }
            catch
            {
                // ignore if any exceptions here
            }
        }
    }
}
