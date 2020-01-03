/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Tools/Logs.cs
Revision        : 1.0
Changelog       :   
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Tools
{

    /// <summary>
    /// Log system
    /// </summary>
    public static class Logs
    {
        private static bool _started = false;
        public const string DEFAULT_LOG = "[Default]";

        /// <summary>
        /// Save a log
        /// </summary>
        /// <param name="message">Message to be saved</param>
        /// <param name="direction">Direction of the message</param>
        /// <param name="chronology">Show time</param>
        /// <param name="logName">Log file name</param>
        public static void SaveLog(string message, string direction, bool chronology = true, string logName = DEFAULT_LOG)
        {
            try
            {
                string logFile = Path.Combine(Application.persistentDataPath, string.Format(string.Format("{0} - {1}", logName, "Log {0}.log"), DateTime.Today.ToString("dd-MM-yyyy")));
                if (_started)
                {
                    string text = string.Format("{0}{0}______________________________ ForgeSDK - Execution Log: {1} ______________________________{0}{0}", Environment.NewLine, DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                    File.AppendAllText(logFile, text);
                    _started = true;
                }

                string final = "";
                if (chronology) final = string.Format("{0}: {1} - {2}{3}{3}", DateTime.Now.ToString("hh:mm:ss"), direction, message, Environment.NewLine);
                else final = string.Format("{0} - {1}{2}{2}", direction, message, Environment.NewLine);

                File.AppendAllText(logFile, final);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError(string.Format("Can't save execution log - {0}", ex.ToString()));
            }
        }

        /// <summary>
        /// Get the name of the funciont with Reflection
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetDirection()
        {
            try
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                return string.Format("{0}.{1}", sf.GetMethod().DeclaringType.Name, sf.GetMethod().Name);
            }
            catch
            {
                return "Direction not found.";
            }
        }
    }
}
