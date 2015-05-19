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

namespace Assignment2_EricAllen_FormChatApp
{
    public partial class mainForm : Form
    {
        private int childFormNumber = 0;
        private Thread chatListenerThread;
        private Thread chatSender;
        private Client client;
        



        public mainForm()
        {

            client = new Client();
            client.MessageReceived += new MessageReceivedHandler(WriteMessage);
            InitializeComponent();
           

        }

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


        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client.ConnectToServer())
            {
                chatListenerThread = new Thread(client.ReceiveMessage);
                chatListenerThread.Name = "Listener";

                chatListenerThread.Start();
                //connectToolStripMenuItem.Enabled = false;
                //disconnectToolStripMenuItem.Enabled = true;
            }
            
            

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            chatSender = new Thread(() =>client.SendMessage(txtMessage.Text.ToString()));
            chatSender.Start();
            txtMessageArea.AppendText(txtMessage.Text + "\r\n");
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            safeDisconnect();

        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            safeDisconnect();
        }

        private void safeDisconnect(){
            client.Close();
            if (chatListenerThread != null)
            {
                chatListenerThread.Join();
            }
            //connectToolStripMenuItem.Enabled = true;
            //disconnectToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            safeDisconnect();
            System.Environment.Exit(0);
        }
    }
}
