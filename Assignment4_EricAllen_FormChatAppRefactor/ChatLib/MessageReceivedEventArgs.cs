using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{

    /// <summary>
    /// Used to return message back to the main form
    /// </summary>
    public class MessageReceivedEventArgs:EventArgs
    {
        String message;
        /// <summary>
        /// sets the message that will be returned
        /// </summary>
        /// <param name="message"></param>
        public MessageReceivedEventArgs(String message)
        {
            this.message = message;

        }

        /// <summary>
        /// returns the message
        /// </summary>
        public String Message
        {
            get { return message; }
        }

    }
}
