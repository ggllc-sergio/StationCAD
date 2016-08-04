using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace StationCAD.Processor
{
    public abstract class BaseManager
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
