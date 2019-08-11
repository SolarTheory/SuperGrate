﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperGrate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] parameters)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.Run(new Main(parameters));

        }
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.Error("\r-----------FATAL ERROR-----------\r", true);
            Logger.Error(e.Exception.Message + "\r", true);
            Logger.Error("\r--------------TRACE--------------\r", true);
            Logger.Error(e.Exception.StackTrace + "\r\r", true);
            if (e.Exception.InnerException != null)
            {
                Logger.Error("\r-----------INNER ERROR-----------\r", true);
                Logger.Error(e.Exception.InnerException.Message + "\r", true);
                Logger.Error("\r-----------INNER TRACE-----------\r", true);
                Logger.Error(e.Exception.InnerException.StackTrace + "\r\r", true);
            }
        }
    }
}
