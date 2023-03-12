using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace server
{
    public partial class Form1 : Form
    {
       static string path_user = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "user-db.txt");
       static string path_sweet = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "sweet-db.txt");
       static string path_follow = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "follow-db.txt");
       static string path_block = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "block-db.txt");
       string[] userList = File.ReadAllLines(path_user);

       Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
       List<Socket> clientSockets = new List<Socket>();
       List<string> onlineUsers = new List<string>();
       bool terminating = false;
       bool listening = false;
        
       int ID = 0;

       public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
            
        }

       private void button_listen_Click(object sender, EventArgs e)     //listen
        {
            int serverPort;
            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");
                try
                {
                    string[] sweetList = File.ReadAllLines(path_sweet);
                    ID = sweetList.Length;
                }
                catch
                {
                    //ID CHECK
                }
            }

            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

       private void Accept()
        {
            while(listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    Byte[] buffer = new Byte[64];
                    newClient.Receive(buffer);
                    string userName = Encoding.Default.GetString(buffer);
                    userName = userName.Substring(0, userName.IndexOf("\0"));
                   
                    logs.AppendText("Client " + userName + " is trying to join\n");
                    bool isOnline = onlineUsers.Contains(userName);                 //check if user is online
                    bool isExist = userList.Contains(userName);                     //check if user exist in database
                    if (isExist && !isOnline)
                    {
                        logs.AppendText("Client: " + userName + " has been joined\n");         //add user 
                        clientSockets.Add(newClient);
                        onlineUsers.Add(userName);
                        Byte[] buffer_reply = Encoding.Default.GetBytes("successful");          //send feedback to client
                        newClient.Send(buffer_reply);


                        Thread receiveThread = new Thread(() => Receive(newClient, userName)); // updated
                        receiveThread.Start();
                    }
               
                    else
                    {
                        Byte[] buffer_reply;
                        if (isOnline)                                            //if user is already online
                        {
                            logs.AppendText(userName + " is already online \n");
                            buffer_reply = Encoding.Default.GetBytes("Albert");    //buzzword for inforing client
                            
                        }
                        else                                                          //If user does not exist
                        {
                            logs.AppendText(userName + " does not exist in database \n");
                            buffer_reply = Encoding.Default.GetBytes("Levi");          //buzzword for informing client
                            
                        }
                        newClient.Send(buffer_reply);
                        newClient.Close();                                    //kick the user out of server as it could not pass the requirements
                    }
                                          
                 
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

       private void Receive(Socket thisClient, string userName) // updated
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    thisClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage.Contains("-*+Feed Request"))               //if user requested feed
                    {
                        bool is_follow_request = incomingMessage.Contains("Followed");

                        List<string> followedUsers = new List<string>();
                        if (File.Exists(path_follow))
                        {
                            string[] followList = File.ReadAllLines(path_follow);
                            foreach (string line in followList)
                            {
                                if (userName == line.Split('-')[0])
                                {
                                    followedUsers.Add(line.Split('-')[1]);
                                }
                            }
                        }

                        //Bloklanan kişiler feed requestte gözükmesin
                        List<string> blockedUsers = new List<string>();
                        if (File.Exists(path_block))
                        {
                            string[] blockList = File.ReadAllLines(path_block);
                            foreach (string line in blockList)
                            {
                                if (userName == line.Split('-')[1])
                                {
                                    blockedUsers.Add(line.Split('-')[0]);
                                }
                            }
                        }

                        try
                        {
                            int count = 0;
                            string[] sweetList = File.ReadAllLines(path_sweet);

                            if (is_follow_request)
                            {
                                string request = userName + "requested Follower Feed\n";
                                logs.AppendText(request);
                            }
                            else
                            {
                                string request = userName + " requested Feed\n";
                                logs.AppendText(request);
                            }

                            foreach (string line in sweetList)                 //parse the sweet list
                            {
                                string sweetOwner = line.Substring(0, line.IndexOf(" "));
                                if (is_follow_request)
                                {
                                    if (followedUsers.Contains(sweetOwner) && !blockedUsers.Contains(sweetOwner))
                                    {
                                        Byte[] buffer_new = new Byte[128];
                                        buffer_new = Encoding.Default.GetBytes(line);
                                        thisClient.Send(buffer_new);             //send sweet
                                        count++;
                                        Thread.Sleep(50);
                                    }
                                }
                                else
                                {
                                    if (sweetOwner != userName && !blockedUsers.Contains(sweetOwner))              //if sweet does not belongs to user that requested feed
                                    {
                                        Byte[] buffer_new = new Byte[128];
                                        buffer_new = Encoding.Default.GetBytes(line);
                                        thisClient.Send(buffer_new);             //send sweet
                                        count++;
                                        Thread.Sleep(50);    //sleep thread to avoid confusion, one may change its value to increase speed
                                    }
                                }
                            }
                            if (count == 0)           //If user does not have feed
                            {
                                Byte[] buffer_new = new Byte[128];
                                string exception = "There is no sweets\n";
                                buffer_new = Encoding.Default.GetBytes(exception);
                                thisClient.Send(buffer_new);
                            }
                        }
                        catch    //If there is no sweet in other words sweet database txt has not been created
                        {

                            string empty1 = "There is no sweets in database ";
                            Byte[] buffer_empty = new Byte[128];
                            buffer_empty = Encoding.Default.GetBytes(empty1);
                            thisClient.Send(buffer_empty);
                        }
                    }
                    else if (incomingMessage == "-*+User List Request")                 //if user requested feed
                    {
                        string request = userName + " requested users list\n";
                        logs.AppendText(request);

                        string[] userList = File.ReadAllLines(path_user);
                        Byte[] listbuffer = new Byte[128];
                        listbuffer = Encoding.Default.GetBytes("Sending users list\n");
                        thisClient.Send(listbuffer);
                        foreach (string line in userList)                 //parse the sweet list
                        {
                            Byte[] buffer_new = new Byte[128];
                            string sendline = line + "\n";
                            buffer_new = Encoding.Default.GetBytes(sendline);
                            thisClient.Send(buffer_new);             //send sweet

                            Thread.Sleep(2);    //sleep thread to avoid confusion, one may change its value to increase speed
                        }

                    }
                    else if (incomingMessage.Contains("-*+Follow"))
                    {

                        string followed = incomingMessage.Split(' ')[1];
                        bool isItself = userName == followed;                 //check if user is online
                        bool isExist = userList.Contains(followed);
                        bool isFollowed = false;
                        bool isBlocked = false;

                        Byte[] buffer_reply;

                        if (File.Exists(path_follow))
                        {
                            string followers_db = File.ReadAllText(path_follow);
                            isFollowed = followers_db.Contains(userName + "-" + followed + "\n"); //zaten takip ediyor muyuz?
                        }
                        if (File.Exists(path_block))
                        {
                            string block_db = File.ReadAllText(path_block);
                            isBlocked = block_db.Contains(followed + "-" + userName + "\n"); //blocklandık mı?
                        }

                        if (isExist && !isItself && !isFollowed && !isBlocked)
                        {
                            logs.AppendText(text: $"{userName} has followed {followed}\n");
                            File.AppendAllText(path_follow, userName + "-" + followed + "\n");
                            buffer_reply = Encoding.Default.GetBytes("You have followed " + followed + "\n");

                        }
                        else
                        {
                            if (isItself)                                            //if user is already online
                            {
                                logs.AppendText("You cannot follow yourself dear " + userName + "\n");
                                buffer_reply = Encoding.Default.GetBytes("You cannot follow yourself\n");    //buzzword for inforing client
                            }
                            else if (isFollowed)
                            {
                                logs.AppendText(userName + " already follows  " + followed + "\n");
                                buffer_reply = Encoding.Default.GetBytes("You are already following " + followed);
                            }
                            else if (isBlocked)
                            {
                                logs.AppendText(userName + " cannot follow " + followed + " because " + followed + " blocked " + userName + ", \n");
                                buffer_reply = Encoding.Default.GetBytes("You cannot follow " + followed + " because you are blocked");
                            }
                            else                                                          //If user does not exist
                            {
                                logs.AppendText(followed + " does not exist in database\n");
                                buffer_reply = Encoding.Default.GetBytes(followed + " does not exist in database\n");          //buzzword for informing client
                            }
                        }
                        thisClient.Send(buffer_reply);
                    }
                    else if (incomingMessage == "-*+Disconnect")
                    {
                        thisClient.Close();
                        onlineUsers.Remove(userName);
                        clientSockets.Remove(thisClient);
                        connected = false;
                        logs.AppendText(userName + " has disconnected from the server.\n");
                        Thread.Sleep(2);
                    }
                    else if (incomingMessage == "-*+Follwed List Request")
                    {
                        string request = userName + " requested followed list\n";
                        logs.AppendText(request);

                        Byte[] listbuffer = new Byte[128];
                        listbuffer = Encoding.Default.GetBytes("Sending followed users list\nYou are following:\n");
                        thisClient.Send(listbuffer);

                        string[] userList = File.ReadAllLines(path_follow);
                        int counter = 0;
                        foreach (string line in userList)                 //parse the sweet list
                        {
                            if (userName == line.Split('-')[0])
                            {
                                counter++;
                                Byte[] buffer_new = new Byte[128];
                                string sendline = line.Split('-')[1] + "\n";
                                buffer_new = Encoding.Default.GetBytes(sendline);
                                thisClient.Send(buffer_new);

                                Thread.Sleep(2);    //sleep thread to avoid confusion, one may change its value to increase speed
                            }
                        }
                        if (counter == 0)
                        {
                            Byte[] buffer_new = new Byte[128];
                            buffer_new = Encoding.Default.GetBytes("You are not following anybody\n");
                            thisClient.Send(buffer_new);

                            Thread.Sleep(2);
                        }
                        Byte[] buffer_new2 = new Byte[128];
                        buffer_new2 = Encoding.Default.GetBytes("In total, you are following " + counter.ToString() + " users\n");
                        thisClient.Send(buffer_new2);

                    }

                    else if (incomingMessage.Contains("-*+Block"))
                    {
                        string to_be_blocked = incomingMessage.Split(' ')[1];
                        bool isItself = userName == to_be_blocked;                 //check if user is online
                        bool isExist = userList.Contains(to_be_blocked);
                        bool isBlocked = false;

                        Byte[] buffer_reply;
                        if (File.Exists(path_block))
                        {
                            string block_db = File.ReadAllText(path_block);
                            isBlocked = block_db.Contains(userName + "-" + to_be_blocked + "\n"); //zaten engelledik mi?
                        }

                        if (isExist && !isItself && !isBlocked)
                        {
                            logs.AppendText(text: $"{userName} has blocked {to_be_blocked}\n");
                            File.AppendAllText(path_block, userName + "-" + to_be_blocked + "\n");
                            buffer_reply = Encoding.Default.GetBytes("You have blocked " + to_be_blocked + "\n");

                        }
                        else
                        {
                            if (isItself)                                            //if user is already online
                            {
                                logs.AppendText("You cannot block yourself dear " + userName + "\n");
                                buffer_reply = Encoding.Default.GetBytes("You cannot block yourself\n");    //buzzword for inforing client
                            }
                            else if (isBlocked)
                            {
                                logs.AppendText(userName + " has already blocked  " + to_be_blocked + "\n");
                                buffer_reply = Encoding.Default.GetBytes("You have already blocked " + to_be_blocked);
                            }


                            else                                                          //If user does not exist
                            {
                                logs.AppendText(to_be_blocked + " does not exist in database\n");
                                buffer_reply = Encoding.Default.GetBytes(to_be_blocked + " does not exist in database\n");          //buzzword for informing client
                            }
                        }


                        if (File.Exists(path_follow))
                        {
                   
                            var lines1 = File.ReadAllLines(path_follow).Where(line => !(line.Split('-')[0] == to_be_blocked && line.Split('-')[1] == userName)).ToArray();
                            File.WriteAllLines(path_follow, lines1);

                        }

                        thisClient.Send(buffer_reply);

                    }
                    else if (incomingMessage.Contains("-*+Delete Request"))
                    {
                        string request = userName + " requested delete a sweet\n";
                        logs.AppendText(request);
                        string to_be_deleted = incomingMessage.Split(' ')[2];

                        string[] sweetList;
                        string found = String.Empty;
                        Byte[] buffer_reply;
                        if (File.Exists(path_sweet))
                        {
                            sweetList = File.ReadAllLines(path_sweet);
                            foreach (string line in sweetList)
                            {
                                if (line.Split(':')[3].Trim() == to_be_deleted)
                                {
                                    found = line;
                                }
                            }

                            if (found == String.Empty)
                            {
                                logs.AppendText("The Sweet " + userName + " requested to delete does not exist in the database\n");
                                buffer_reply = Encoding.Default.GetBytes("The Sweet you requested to delete does not exist in the databese\n");
                            }
                            else if (found.Split(' ')[0] != userName)
                            {
                                logs.AppendText(userName + " cannot delete the sweet with id: " + to_be_deleted + " because that sweet belongs to a different user.\n");
                                buffer_reply = Encoding.Default.GetBytes("You cannot delete the sweet with the id: " + to_be_deleted.ToString() + " because you are not the owner of that sweet\n");
                            }
                            else
                            {
                                logs.AppendText("The delete operation " + userName + " requested has been done successfuly.\n");
                                buffer_reply = Encoding.Default.GetBytes("You have successfuly deleted the sweet with id: " + to_be_deleted.ToString() + "\n");

                                var lines = File.ReadAllLines(path_sweet).Where(line => !(line.Split(' ')[0] == userName && line.Split(':')[3].Trim() == to_be_deleted)).ToArray();
                                File.WriteAllLines(path_sweet, lines);
                            }
                        }
                        else
                        {
                            logs.AppendText("There are no sweets in the database. Delete operation " + userName + " requested cannot be done.\n");
                            buffer_reply = Encoding.Default.GetBytes("You cannot perform a delete operation because there are no sweets in the database.\n");
                        }
                        thisClient.Send(buffer_reply);
                    }
                    else if (incomingMessage == "-*+Follwer List Request")
                    {
                        string request = userName + " requested follower list\n";
                        logs.AppendText(request);

                        Byte[] listbuffer = new Byte[128];
                        listbuffer = Encoding.Default.GetBytes("Sending follower users list\nThese users are following you:\n");
                        thisClient.Send(listbuffer);

                        string[] followList = File.ReadAllLines(path_follow);
                        int counter = 0;
                        foreach (string line in followList)                 //parse the sweet list
                        {
                            if (userName == line.Split('-')[1])
                            {
                                counter++;
                                Byte[] buffer_new = new Byte[128];
                                string sendline = line.Split('-')[0] + "\n";
                                buffer_new = Encoding.Default.GetBytes(sendline);
                                thisClient.Send(buffer_new);

                                Thread.Sleep(2);    //sleep thread to avoid confusion, one may change its value to increase speed
                            }
                        }
                        if (counter == 0)
                        {
                            Byte[] buffer_new = new Byte[128];
                            buffer_new = Encoding.Default.GetBytes("Nobody is following you :(\n");
                            thisClient.Send(buffer_new);

                            Thread.Sleep(2);
                        }
                        Byte[] buffer_new2 = new Byte[128];
                        buffer_new2 = Encoding.Default.GetBytes("In total, you have " + counter.ToString() + " followers\n");
                        thisClient.Send(buffer_new2);
                    }
                    else if (incomingMessage == "-*+Get Own Sweets")
                    {
                        try
                        {
                            int counter = 0;
                            string[] sweetList = File.ReadAllLines(path_sweet);
                            foreach (string line in sweetList)                 //parse the sweet list
                            {
                                string sweetOwner = line.Substring(0, line.IndexOf(" "));
                                if (sweetOwner == userName)              //if sweet does not belongs to user that requested feed
                                {
                                    Byte[] buffer_new = new Byte[128];
                                    buffer_new = Encoding.Default.GetBytes(line);
                                    counter++;
                                    thisClient.Send(buffer_new);             //send sweet
                                    Thread.Sleep(50);    //sleep thread to avoid confusion, one may change its value to increase speed
                                }
                            }
                            if (counter == 0)
                            {
                                Byte[] buffer_new = new Byte[128];
                                buffer_new = Encoding.Default.GetBytes("You haven't posted any sweets.\n");
                                thisClient.Send(buffer_new);
                                Thread.Sleep(2);
                            }
                        }
                        catch
                        {
                            Byte[] buffer_new = new Byte[128];
                            buffer_new = Encoding.Default.GetBytes("There is no sweets in the database.\n");
                            thisClient.Send(buffer_new);
                            Thread.Sleep(2);
                        }
                    }
                    else      // some user sent sweet so store it in file
                    {
                        DateTime now = DateTime.Now;
                        string date = now.ToString("f");
                        string sweet = userName + " posted: " + incomingMessage + " @ " + date + ", Sweet ID: " + ID.ToString();
                        File.AppendAllText(path_sweet, sweet + "\n");
                        logs.AppendText(sweet + "\n");      //inform server that sweet has been posted
                        ID++;
                    }
                }
                catch
                {
                    if(!terminating)
                    {
                        logs.AppendText(userName +" has disconnected\n");
                        onlineUsers.Remove(userName);
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

       private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }
    }
}