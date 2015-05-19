using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    public class MessageReceivedEventArgs:EventArgs
    {
        String message;

        public MessageReceivedEventArgs(String message)
        {
            this.message = message;

        }

        public String Message
        {
            get { return message; }
        }

    }
}
