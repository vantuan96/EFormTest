﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CustomLogs
    {
        public static readonly NLog.Logger accesslog = NLog.LogManager.GetLogger("accesslogger");
        public static readonly NLog.Logger errorlog = NLog.LogManager.GetLogger("errorlogger");
        public static readonly NLog.Logger apigwlog = NLog.LogManager.GetLogger("apigwlogger");
        public static readonly NLog.Logger intervaljoblog = NLog.LogManager.GetLogger("intervaljoblogger");
        public static readonly NLog.Logger apipubliblog = NLog.LogManager.GetLogger("apipubliblog");
        private static CustomLogs _instant { get; set; }
        public static CustomLogs Instant
        {
            get
            {
                if (_instant == null)
                    _instant = new CustomLogs();
                return _instant;
            }
            set
            {
                _instant = value;
            }
        }
        public void IntervalJobLog(string sMsg, string logType, bool printConsole = false)
        {
            if (logType == Constants.Log_Type_Info)
            {
                if (printConsole)
                    Console.WriteLine(sMsg);
                intervaljoblog.Info(sMsg);
            }
            else if (logType == Constants.Log_Type_Debug)
            {
                if (printConsole)
                    Console.WriteLine(sMsg);
                intervaljoblog.Debug(sMsg);
            }
            else if (logType == Constants.Log_Type_Error)
            {
                if (printConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(sMsg);
                    Console.ResetColor();
                }
                intervaljoblog.Error(sMsg);
            }
        }
        public void ErrorLog(string sMsg, string logType, bool printConsole = false)
        {
            if (logType == Constants.Log_Type_Info)
            {
                if (printConsole)
                    Console.WriteLine(sMsg);
                errorlog.Info(sMsg);
            }
            else if (logType == Constants.Log_Type_Debug)
            {
                if (printConsole)
                    Console.WriteLine(sMsg);
                errorlog.Debug(sMsg);
            }
            else if (logType == Constants.Log_Type_Error)
            {
                if (printConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(sMsg);
                    Console.ResetColor();
                }
                errorlog.Error(sMsg);
            }
        }
    }
}
