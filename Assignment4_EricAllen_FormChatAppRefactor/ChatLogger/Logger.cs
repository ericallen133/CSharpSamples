using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Interfaces;

namespace ChatLogger
{
    public class Logger : ILoggingService
    {

        String fileName;

        /// <summary>
        /// Sets the file name to log to/
        /// </summary>
        public Logger()
        {
            fileName = System.Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") +".txt";
        }


        /// <summary>
        /// Writes input to the log file
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            using (System.IO.StreamWriter streamOut = new System.IO.StreamWriter(fileName, true))
            {
                streamOut.WriteLine(DateTime.Now.ToString() + " :" + message + streamOut.NewLine);
            }
        }
    }
}
