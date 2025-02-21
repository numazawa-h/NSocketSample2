using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "./log4net.xml")]

namespace NCommonUtility
{
    internal class Log
    {

        // シングルトン
        static private Log _instance = null;
        static public Log GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Log();
            }
            return _instance;
        }
        private Log()
        {
        }

        static private log4net.ILog _logger = null;
        static bool _isTrace = false;
        static private int _traceLevel = -1;
        static public void  Init(int traceLevel=-1)
        {
            _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            var rootLogger = ((Hierarchy)_logger.Logger.Repository).Root;
            _isTrace =(rootLogger.Level == Level.Trace);
            _traceLevel = traceLevel;
        }


        static public void Info(string message)
        {
            Log._logger.Info(message);
        }
        static public void Debug(string message)
        {
            Log._logger.Debug(message);
        }
        static public void Warn(string message, Exception ex = null)
        {
            if (ex == null)
            {
                Log._logger.Warn(message);
            }
            else
            {
                Log._logger.Warn(message, ex);
            }
            Log._logger.Warn("\r\n");
        }
        static public void Error(string message, Exception ex = null)
        {
            if (ex == null) {
                Log._logger.Error(message);
            }
            else
            {
                Log._logger.Error(message, ex);
            }
            Log._logger.Error("\r\n");
        }

        static public void Trace(string arg = null, int trace_type =-1, [CallerMemberName] string callerMethodName = null, [CallerLineNumber] int line = -1)
        {
            if (_isTrace == false) return;
            if (_traceLevel ==0) return;
            if ((trace_type & _traceLevel) == 0) return;

            if (callerMethodName !=null)
            {
                if(arg != null)
                {
                    Log._logger.Debug($"----- Call '{callerMethodName}'({arg}) at line {line}");
                }
                else
                {
                    Log._logger.Debug($"----- Call '{callerMethodName}' at line {line}");
                }
            }
        }
    }
}
