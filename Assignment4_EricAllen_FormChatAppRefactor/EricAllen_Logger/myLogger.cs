using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using LogThis;

namespace EricAllen_Logger
{
    public class myLogger : ILoggingService
    {
        public myLogger()
        {
            LogThis.Log.UseSensibleDefaults(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"),System.Environment.CurrentDirectory, eloglevel.verbose);

        }

        public void Log(string message)
        {
            LogThis.Log.LogThis(message, eloglevel.verbose);
        }
    }
}
