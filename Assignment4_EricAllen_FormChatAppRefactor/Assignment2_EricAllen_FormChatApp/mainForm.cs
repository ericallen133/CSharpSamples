using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ChatLib;
using Interfaces;

namespace Assignment2_EricAllen_FormChatApp
{
    public partial class mainForm : Form
    {
        private int childFormNumber = 0;
        private Thread chatListenerThread;
        private Thread chatSender;
        private Client client;
        


        /// <summary>
        /// Load main form, create necessary objects
        /// </summary>
        public mainForm(ILoggingService logger)
        {

            client = new Client(logger);
            client.MessageReceived += new MessageReceivedHandler(WriteMessage);
            InitializeComponent();
           

        }


        /// <summary>
        /// writes a message to the main message box
        /// </summary>
        /// <param name="mre"></param>
        private void WriteMessage(ChatLib.MessageReceivedEventArgs mre)
        {
            if (mre.Message.Length > 0)
            {
                if (txtMessageArea.InvokeRequired)
                {
                    MethodInvoker myMethod = new MethodInvoker(delegate
                    {
                        txtMessageArea.AppendText(">>" + mre.Message + "\r\n");
                    });
                    txtMessageArea.BeginInvoke(myMethod);
                }
                else
                {
                    txtMessageArea.AppendText(">>" +mre.Message + "\r\n");
                }
            }
        }
       
        /// <summary>
        /// Connects to the server and starts the listening thread for incomming messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client.ConnectToServer())
            {
                chatListenerThread = new Thread(client.ReceiveMessage);
                chatListenerThread.Name = "Listener";

                chatListenerThread.Start();
                //connectToolStripMenuItem.Enabled = false;
                disconnectToolStripMenuItem.Enabled = true;
            }           
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            chatSender = new Thread(() =>client.SendMessage(txtMessage.Text.ToString()));
            chatSender.Start();
            txtMessageArea.AppendText(txtMessage.Text + "\r\n");
        }

        /// <summary>
        /// Disconnects from the server and stops the thread if it's running
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            safeDisconnect();
        }

        /// <summary>
        /// Disconnects from the server and stops the thread if it's running before closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            safeDisconnect();
        }

        /// <summary>
        /// Disconnects from the server and stops the thread if it's running, disables the disconnect button
        /// </summary>
        private void safeDisconnect(){
            client.Close();
            if (chatListenerThread != null)
            {
                chatListenerThread.Join();
            }
            if (chatSender != null)
            {
                chatSender.Join();
            }
            //connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Disconnects from the server and stops the thread if it's running before closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            safeDisconnect();
            System.Environment.Exit(0);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
