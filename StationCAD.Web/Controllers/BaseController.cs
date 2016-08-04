using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace StationCAD.Web.Controllers
{
    public abstract class BaseController : Controller
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected void LogInfo(string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        protected void LogException(string message, Exception ex)
        {
            logger.Log(LogLevel.Error, ex, message);
        }

    }
}