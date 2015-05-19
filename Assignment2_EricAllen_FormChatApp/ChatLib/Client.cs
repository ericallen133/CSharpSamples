using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ChatLogger;

namespace ChatLib
{
    public class Client
    {
        public MessageReceivedHandler MessageReceived;

        private static Int32 port;
        protected bool isConnected;
        protected TcpClient client;
        Byte[] bytes = new Byte[256];
        protected NetworkStream stream;
        private static IPAddress localAddress;
        Logger log;

        public Client()
        {
            port = 13000;
            log = new Logger();
            localAddress = IPAddress.Parse("127.0.0.1");
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsConnected
        {
            get { return isConnected; }
        }


        public Int32 currentPort
        {
            get { return port; }
            set { port = value; }
        }

        public IPAddress CurrentAddress
        {
            get { return localAddress; }
            set { localAddress = value; }
        }

        public bool ConnectToServer()
        {
            if (client == null)
            {
                try
                {
                    log.writeLog("Connected to server on port " + port + ", and ip " + localAddress);
                    this.client = new TcpClient(CurrentAddress.ToString(), currentPort);
                    this.stream = client.GetStream();
                    isConnected = true;
                    MessageReceived(new MessageReceivedEventArgs("Successfully connected to server"));
                    
                    return true;
                }
                catch (SocketException se)
                {
                    Close();
                }
                
            }
            MessageReceived(new MessageReceivedEventArgs("Connection failed"));
            return false;
        }

        /// <summary>
        /// Attempts to read a single byte from the stream, on failure closes the stream and disconnects the client/server
        /// </summary>
        public void CheckConnection()
        {
            try
            {
                if (client.Client.Poll(0, SelectMode.SelectRead))
                {
                    byte[] buff = new byte[1];
                    if (client.Client.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        Close();
                    }

                }
            }
            catch (SocketException se)
            {

                Close();
            }
            catch (Exception e)
            {
                Close();
            }

        }


        /// <summary>
        /// Sends a message to the opposite listener
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(String message)
        {
            if (isConnected)
            {
                log.writeLog(message);
                bytes = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(bytes, 0, bytes.Length);
            }
        }


        /// <summary>
        /// Checks the stream for any messages, if there is a message to be displayed, 
        /// reads the stream to completion and returns the decoded string
        /// </summary>
        /// <returns></returns>
        public void ReceiveMessage()
        {
            String data = "";
            int i;
            while (true)
            {
                CheckConnection();
                if (isConnected)
                {
                    try
                    {

                        if (stream.DataAvailable)
                        {
                            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                data += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                                if (!stream.DataAvailable)
                                {
                                    break;
                                }
                            }
                            log.writeLog(data);
                            MessageReceived(new MessageReceivedEventArgs(data));
                            
                            data = "";
                        }
                    }
                    catch (SocketException se)
                    {
                        Close();
                    }
                    catch (Exception e)
                    {
                        Close();
                    }
                   
                   
                }
                else
                {
                    break;
                }
            }
        }


        /// <summary>
        /// Sets the flag for connection to false and closes the stream
        /// </summary>
        public void Close()
        {
            isConnected = false;
            if (stream != null)
            {
                isConnected = false;
                //client.GetStream().Close();
                log.writeLog("Disconnected from server");
                stream.Close();
                stream = null;
                client.Close();
                client = null;
                MessageReceived(new MessageReceivedEventArgs("Disconnected from server"));
            }
        }

    }
}
