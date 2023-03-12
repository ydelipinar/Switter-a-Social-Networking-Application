using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        string userName = "";
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;

            int portNum;
            if(Int32.TryParse(textBox_port.Text, out portNum))     //parse the ip taken by GUI
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    connected = true;
                
                    userName = textBox_UserName.Text;

                    if (userName!= "" && userName.Length <= 64)
                    {
                        Byte[] userName_buffer = Encoding.Default.GetBytes(userName);   //send the username to the server
                        clientSocket.Send(userName_buffer);
                    }

                    Byte[] userName_validation_buffer = new Byte[64];               //get userName validation from the server,
                    clientSocket.Receive(userName_validation_buffer);
                    string validation = Encoding.Default.GetString(userName_validation_buffer);
                    validation = validation.Substring(0, validation.IndexOf("\0"));

                    if (validation == "successful")       //If entered userName is in database and not online
                    {
                        button_connect.Enabled = false;
                        textBox_sweet.Enabled = true;
                        button_send.Enabled = true;

                        connected = true;
                        terminating = false;

                        button_feed_request.Enabled = true;
                        button_followed_feed.Enabled = true;
                        button_follow.Enabled = true;
                        button_user_list.Enabled = true;
                        button_disconnect.Enabled = true;
                        textBox_follow.Enabled = true;
                        button_delete.Enabled = true;
                        button_get_own_sweets.Enabled = true;
                        button_block.Enabled = true;
                        textBox_block.Enabled = true;
                        textBox_delete.Enabled = true;
                        button_followed_list.Enabled = true;
                        button_follower_list.Enabled = true;
                        logs.AppendText("Connected to the server!\n");
                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();
                    }
                    else     //user can not join
                    {
                        if(validation == "Albert")                //If server sends Albert it means user is already online
                        {
                            logs.AppendText("This user is already online \n");
                        }
                        else                                       //username is not in database
                        {
                            logs.AppendText("This user does not exist in database \n");
                        }                    
                    }

                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }

        }
        
        private void Receive()
        {
            while(connected)             // while connected receive input from user
            {
                try
                {
                    Byte[] text_file_line_buffer = new Byte[128];
                    clientSocket.Receive(text_file_line_buffer);

                    string incomingLine = Encoding.Default.GetString(text_file_line_buffer);
                    incomingLine = incomingLine.Substring(0, incomingLine.IndexOf("\0"));
                    logs.AppendText(incomingLine + "\n");

                }
                catch
                {
                    if (!terminating) //&& connected eklendi
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                        button_disconnect.Enabled = false;
                        button_follow.Enabled = false;
                        button_followed_feed.Enabled = false;
                        button_send.Enabled = false;
                        button_feed_request.Enabled = false;
                        button_user_list.Enabled = false;
                        button_delete.Enabled = false;
                        button_get_own_sweets.Enabled = false;
                        button_block.Enabled = false;
                        button_followed_list.Enabled = false;
                        button_follower_list.Enabled = false;
                        button_get_own_sweets.Enabled = false;

                        textBox_block.Clear();
                        textBox_delete.Clear();
                        textBox_follow.Clear();
                        textBox_ip.Clear();
                        textBox_port.Clear();
                        textBox_sweet.Clear();
                        textBox_UserName.Clear();

                        textBox_block.Enabled = false;
                        textBox_delete.Enabled = false;
                        textBox_follow.Enabled = false;
                        textBox_sweet.Enabled = false;
                        textBox_follow.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }
            }
        }
        
        private void button_send_Click(object sender, EventArgs e)     //send sweet
        {
            string sweet = textBox_sweet.Text;

            if(sweet != "" && sweet.Length <= 128)
            {
                
                Byte[] buffer = new Byte[128]; 
                buffer = Encoding.Default.GetBytes(sweet);
                logs.AppendText("Sweet send.\n");
                textBox_sweet.Clear();
                clientSocket.Send(buffer);
            }
        }
        
        private void button_feed_request_Click(object sender, EventArgs e)  //send feed request
        {
            string request = "-*+Feed Request";
            Byte[] request_buffer = Encoding.Default.GetBytes(request);
            clientSocket.Send(request_buffer);
        }
        
        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }
        
        private void button_followed_feed_Click(object sender, EventArgs e)
        {
            string request = "Followed -*+Feed Request";
            Byte[] request_buffer = Encoding.Default.GetBytes(request);
            clientSocket.Send(request_buffer);
        }

        private void button_user_list_Click(object sender, EventArgs e)
        {
            string request = "-*+User List Request";
            Byte[] request_buffer = Encoding.Default.GetBytes(request);
            clientSocket.Send(request_buffer);
        }

        private void button_follow_Click(object sender, EventArgs e)
        {
            string to_be_followed = textBox_follow.Text;
            string request = "-*+Follow " + to_be_followed;
            Byte[] request_buffer = Encoding.Default.GetBytes(request);
            textBox_follow.Clear();
            clientSocket.Send(request_buffer);
       
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            string disconnect_request = "-*+Disconnect";
            Byte[] disconnect_request_buffer = Encoding.Default.GetBytes(disconnect_request);
            clientSocket.Send(disconnect_request_buffer);

            connected = false;
            terminating = true;

            button_connect.Enabled = true;
            button_disconnect.Enabled = false;
            button_follow.Enabled = false;
            button_followed_feed.Enabled = false;
            button_send.Enabled = false;
            button_feed_request.Enabled = false;
            button_user_list.Enabled = false;
            button_delete.Enabled = false;
            button_get_own_sweets.Enabled = false;
            button_block.Enabled = false;
            button_followed_list.Enabled = false;
            button_follower_list.Enabled = false;

            textBox_block.Clear();
            textBox_delete.Clear();
            textBox_follow.Clear();
            textBox_ip.Clear();
            textBox_port.Clear();
            textBox_sweet.Clear();
            textBox_UserName.Clear();

            textBox_block.Enabled = false;
            textBox_delete.Enabled = false;
            textBox_follow.Enabled = false;
            textBox_sweet.Enabled = false;
            textBox_follow.Enabled = false;

            clientSocket.Close();

            logs.AppendText("You have disconnected from the server.\n");

        }

        private void button_followed_list_Click(object sender, EventArgs e)
        {
            string followed_list_request = "-*+Follwed List Request";
            Byte[] followed_list_request_buffer = Encoding.Default.GetBytes(followed_list_request);
            clientSocket.Send(followed_list_request_buffer);
        }

        private void button_follower_list_Click(object sender, EventArgs e)
        {
            string followed_list_request = "-*+Follwer List Request";
            Byte[] followed_list_request_buffer = Encoding.Default.GetBytes(followed_list_request);
            clientSocket.Send(followed_list_request_buffer);
        }

        private void button_block_Click(object sender, EventArgs e)
        {
            string to_be_blocked = textBox_block.Text;
            string request = "-*+Block " + to_be_blocked;
            Byte[] block_request_buffer = Encoding.Default.GetBytes(request);
            textBox_block.Clear();
            clientSocket.Send(block_request_buffer);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string to_be_deleted = textBox_delete.Text;
            string request = "-*+Delete Request " + to_be_deleted;
            Byte[] delete_request_buffer = Encoding.Default.GetBytes(request);
            clientSocket.Send(delete_request_buffer);
            textBox_delete.Clear();
        }

        private void button_get_own_sweets_Click(object sender, EventArgs e)
        {
            string request = "-*+Get Own Sweets";
            Byte[] request_buffer = Encoding.Default.GetBytes(request);
            clientSocket.Send(request_buffer);
        }
    }
}
