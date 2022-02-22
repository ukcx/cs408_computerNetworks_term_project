/*
Group members:
Ugur Kagan Cakir
Ali Erinc Ayaz
Goktug Caliskan
Alpaslan Hamzaoglu
*/
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

using System.Reflection;

namespace client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;         //connected to the server
        bool disconnected = false;      //only becomes true when disconnect button is clicked, when reconnected becomes false
        Socket clientSocket;
        string username;                //user name of the client
        string currentTextBoxInUse = "none";    //to switch between output text boxes

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
            username = textBox_username.Text;

            int portNum;
            if (!Int32.TryParse(textBox_port.Text, out portNum))         //check inputs
            {
                logs.AppendText("Please check port number \n");
            }
            else if (IP == "")
            {
                logs.AppendText("Ip address slot can not be empty\n");
            }
            else if (username == "" || username.Length > 64)
            {
                logs.AppendText("User name is not in a valid length\n");
            }
            else                                            //inputs are valid
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    checkConnection();                      //check whether server accepts this client
                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
        }

        private void checkConnection()  //send the user name and receive feedback from server, if connection is successful start receiving
        {
            try
            {
                Byte[] buffer = Encoding.Default.GetBytes(username);    //send username to server
                clientSocket.Send(buffer);

                Byte[] buffer_2 = new Byte[1];                          //receive feedback from the server
                clientSocket.Receive(buffer_2);                         //true or false

                if (buffer_2[0] > 0)    //if true
                {
                    button_connect.Enabled = false;
                    textBox_ip.Enabled = false;
                    textBox_port.Enabled = false;
                    textBox_username.Enabled = false;

                    followButton.Enabled = true;
                    followBox.Enabled = true;
                    listUsers.Enabled = true;
                    followedSweets.Enabled = true;
                    textBox_message.Enabled = true;
                    button_post.Enabled = true;
                    button_request.Enabled = true;
                    button_disconnect.Enabled = true;
                    button_get_sweets.Enabled = true;
                    button_delete.Enabled = true;
                    button_get_followed.Enabled = true;
                    button_get_follower.Enabled = true;
                    button_refresh.Enabled = true;
                    MySweets.Enabled = true;
                    textBox_block.Enabled = true;
                    button_block.Enabled = true;

                    connected = true;
                    disconnected = false;
                    logs.AppendText("Connected to the server!\n");

                    Thread receiveThread = new Thread(Receive);         //we are connected to the server, we can start receiving messages
                    receiveThread.Start();
                }
                else    //if false
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server did not accept the user name!\n");
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_post.Enabled = false;
                    }
                    clientSocket.Close();
                    connected = false;
                }

            }
            catch
            {
                logs.AppendText("The server did not reply back about connection!\n");
            }
        }

        private void Receive()
        {
            while (connected)    //continuously check for messages sent from server
            {
                try
                {
                    Byte[] buffer = new Byte[256];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (currentTextBoxInUse == "listUsers")        //if we are listing all users we need to use users text box
                    {
                        users.AppendText(incomingMessage + "\n");
                    }
                    else if (currentTextBoxInUse == "refresh")    //if we are listing our sweers we need to use MySweets text box
                    {
                        incomingMessage = incomingMessage.Replace("\n", ", ");
                        MySweets.Items.Add(incomingMessage, CheckState.Unchecked);
                    }
                    else if (currentTextBoxInUse == "followList")  //if we are listing following/followed users
                    {
                        follow_list.AppendText(incomingMessage + "\n");
                    }
                    else        //otherwise use logs
                    {
                        logs.AppendText(incomingMessage + "\n");
                    }
                }
                catch
                {
                    if (!terminating && !disconnected)  //lost connection, but disconnect button has not been clicked
                    {
                        logs.AppendText("The server has disconnected\n");
                        followButton.Enabled = false;
                        followBox.Enabled = false;
                        listUsers.Enabled = false;
                        followedSweets.Enabled = false;
                        button_disconnect.Enabled = false;
                        button_request.Enabled = false;
                        button_post.Enabled = false;
                        textBox_message.Enabled = false;
                        
                        button_refresh.Enabled = false;
                        MySweets.Enabled = false;
                        button_delete.Enabled = false;
                        textBox_block.Enabled = false;
                        button_block.Enabled = false;
                        button_get_followed.Enabled = false;
                        button_get_follower.Enabled = false;

                        button_connect.Enabled = true;
                        textBox_ip.Enabled = true;
                        textBox_port.Enabled = true;
                        textBox_username.Enabled = true;

                        clientSocket.Close();           //close connection to the socket
                        connected = false;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_post_Click(object sender, EventArgs e)
        {
            currentTextBoxInUse = "default";

            string operation = "post";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);

            string message = textBox_message.Text;

            if (message.Length <= 64)
            {
                Byte[] buffer2 = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer2);                              //send sweet message to the server

                logs.AppendText("\nThe following sweet is posted\n");
                logs.AppendText("Username: " + username + "\n");
                logs.AppendText("Message: " + message + "\n");
                logs.AppendText("Time stamp: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "\n\n");
            }

            MySweets.Items.Clear();      //status of my sweets changed, clear output text box
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            connected = false;
            disconnected = true;
            clientSocket.Close();                   //close connection to the socket

            logs.AppendText("Disconnected from the server\n");
            followButton.Enabled = false;
            followBox.Enabled = false;
            listUsers.Enabled = false;
            followedSweets.Enabled = false;
            button_disconnect.Enabled = false;
            button_request.Enabled = false;
            button_post.Enabled = false;
            textBox_message.Enabled = false;

            button_refresh.Enabled = false;
            MySweets.Enabled = false;
            button_delete.Enabled = false;
            textBox_block.Enabled = false;
            button_block.Enabled = false;
            button_get_followed.Enabled = false;
            button_get_follower.Enabled = false;

            button_connect.Enabled = true;
            textBox_ip.Enabled = true;
            textBox_port.Enabled = true;
            textBox_username.Enabled = true;
        }

        private void button_request_Click(object sender, EventArgs e)
        {
            currentTextBoxInUse = "default";

            string operation = "request";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);                              //get sweet message from the server

            logs.AppendText("\nRequested sweets: \n");
        }

        private void listUsers_Click(object sender, EventArgs e)
        {
            currentTextBoxInUse = "listUsers";

            string operation = "usernames";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);                              //get all users from server

            users.AppendText("\nUsernames: \n\n");
        }

        private void followButton_Click(object sender, EventArgs e)
        {
            currentTextBoxInUse = "default";

            string operation = "follow";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);                              //follow request to a user via server

            string message = followBox.Text;

            if (message.Length <= 64)
            {
                Byte[] buffer2 = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer2);                              //follow user name sent to the server
            }
        }

        private void followedSweets_Click(object sender, EventArgs e)
        {
            currentTextBoxInUse = "default";

            string operation = "followedSweets";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);                              //request sweets from followed users

            logs.AppendText("\nSweets from followed users: \n");
        }

        private void MySweets_SelectedIndexChanged(object sender, EventArgs e)
        {}

        private void button_delete_Click(object sender, EventArgs e)    //delete selected sweets
        {
            currentTextBoxInUse = "default";

            string operation = "deleteSweets";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);

            string selectedItems = "";

            for (int i = 0; i < MySweets.Items.Count; i++)
            {
                CheckState st = MySweets.GetItemCheckState(i);
                if (st == CheckState.Checked)               //item is checked
                {
                    string[] sweetInfo = MySweets.Items[i].ToString().Split(',');
                    string sweetId = sweetInfo[0];        //get sweet id

                    selectedItems += "-" + sweetId;       //add ids with '-' in middle
                }
            }
            selectedItems = selectedItems.Substring(1);     //get rid of '-' at the front

            Byte[] buffer2 = Encoding.Default.GetBytes(selectedItems);
            clientSocket.Send(buffer2);                     //send selected ids

            MySweets.Items.Clear();                         //status of my sweets changed, clear output text box
        }

        private void button_get_follower_Click(object sender, EventArgs e)  //get user names that follows this client
        {
            follow_list.Clear();
            currentTextBoxInUse = "followList";
            
            string operation = "sendUserNamesFollowingMe";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);

            follow_list.AppendText("My Followers:" + "\n");
        }

        private void button_get_followed_Click(object sender, EventArgs e)  //get user names that this client follows to
        {
            follow_list.Clear();
            currentTextBoxInUse = "followList";

            string operation = "sendFollowedUserNames";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);

            follow_list.AppendText("Followed Users:" + "\n");
        }

        private void button_refresh_Click(object sender, EventArgs e)   //refresh the my sweets list box, get sweets posted by this client
        {
            MySweets.Items.Clear();
            currentTextBoxInUse = "refresh";

            string operation = "usersSweets";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);                             
        }

        private void button_block_Click(object sender, EventArgs e)
        {
            currentTextBoxInUse = "default";

            string operation = "blockUser";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);
        }

    }
}
