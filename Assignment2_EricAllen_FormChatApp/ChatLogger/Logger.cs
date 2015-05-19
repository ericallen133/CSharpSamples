using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChatLogger
{
    public class Logger
    {

        String fileName;
        public Logger()
        {
            
            fileName = System.Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") +".txt";


        }

        public void writeLog(String log)
        {
            using (System.IO.StreamWriter streamOut = new System.IO.StreamWriter(fileName, true))
            {


                streamOut.WriteLine(DateTime.Now.ToString() + " :" + log + streamOut.NewLine);

            }
        }


    }
}
