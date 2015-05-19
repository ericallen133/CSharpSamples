using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ChatLogger;
using Interfaces;

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
        ILoggingService log;


        /// <summary>
        /// Sets the default port and address
        /// </summary>
        public Client(ILoggingService log)
        {
            port = 13000;
            this.log = log;
            localAddress = IPAddress.Parse("127.0.0.1");
        }

        /// <summary>
        /// Get the status of the connection
        /// </summary>
        public bool IsConnected
        {
            get { return isConnected; }
        }

        /// <summary>
        /// Property of the port variable
        /// </summary>
        public Int32 currentPort
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Property of the localAddress
        /// </summary>
        public IPAddress CurrentAddress
        {
            get { return localAddress; }
            set { localAddress = value; }
        }

        /// <summary>
        /// If the client isn't already connected, connects and logs the the connection, if not logs failure. Writes response to
        /// main text area.
        /// </summary>
        /// <returns></returns>
        public bool ConnectToServer()
        {
            if (client == null)
            {
                try
                {
                    log.Log("Connected to server on port " + port + ", and ip " + localAddress);
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
            log.Log("Could not connect to server");
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
        /// If the client is connected sends a message
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(String message)
        {
            if (isConnected)
            {
                log.Log(message);
                bytes = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(bytes, 0, bytes.Length);
            }
        }


        /// <summary>
        /// Main listening loop, checks for connectivity then checks for data, if there is data signals for the message to be output to
        /// the main text area in the main form
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
                            log.Log(data);
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
        /// Sets the flag for connection to false and closes the stream sends message to text area of disconnection.
        /// </summary>
        public void Close()
        {
            isConnected = false;
            if (stream != null)
            {
                isConnected = false;
                //client.GetStream().Close();
                log.Log("Disconnected from server");
                stream.Close();
                stream = null;
                client.Close();
                client = null;
                MessageReceived(new MessageReceivedEventArgs("Disconnected from server"));
            }
        }

    }
}
