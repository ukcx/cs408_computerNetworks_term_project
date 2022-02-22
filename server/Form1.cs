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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace server
{
    public partial class Form1 : Form
    {
        struct clientInformation                //stores username and socket of the client together
        {
            public string userName;
            public Socket clientSocket;
            public clientInformation(string name, Socket socket) { userName = name; clientSocket = socket; }
        }

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<clientInformation> clientSockets = new List<clientInformation>();         //store info about connected clients in a list

        bool terminating = false;
        bool listening = false;
        int sweet_id = 0;                   //unique ID for received sweet posts

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private int get_sweet_id()          //get sweet id for the next sweet
        {
            string filename = @"sweets.txt";
            if (System.IO.File.Exists(filename))
            {
                if (new FileInfo(filename).Length == 0) //no sweets posted yet
                {
                    return 0;
                }
                else
                {
                    var lines = File.ReadAllLines(filename).Reverse();  //read the file in reverse to find the last id
                    int count = 0;
                    int lastID = 0;
                    foreach (string line in lines)
                    {
                        if (count == 4)
                        {
                            lastID = Int32.Parse(line);
                            break;
                        }
                        count++;
                    }
                    return lastID + 1;      //next id = last id + 1
                }
            }
            else
            {
                return 0;   //no sweets posted yet
            }
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            sweet_id = get_sweet_id();
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))   //port number needs to be an integer
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                textBox_port.Enabled = false;
                listening = true;
                button_listen.Enabled = false;

                Thread acceptThread = new Thread(Accept);           //start accepting clients
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    checkUserName(newClient);                   //after accepting a client; check user name, if user name is 
                                                                //unaccaptable, disconnect the client
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

        private void checkUserName(Socket newClient)
        {
            Byte[] buffer = new Byte[64];
            newClient.Receive(buffer);                              //receive user name from client
            string user_name = Encoding.Default.GetString(buffer);
            user_name = user_name.Substring(0, user_name.IndexOf("\0"));

            if (clientSockets.Any(client => client.userName == user_name))      //a client with this user name has already connected
            {
                sendBoolean(false, newClient);  //give feedback to the client, connection is disabled

                logs.AppendText("Another client with the same username is already connected, client is not accepted.\n");
                newClient.Close();
            }
            else
            {
                string fileName = "../../user-db.txt";
                string[] lines = File.ReadAllLines(fileName);

                if (!lines.Contains(user_name))     //this user name does not exist in the user database
                {
                    sendBoolean(false, newClient);      //give feedback to the client, connection is disabled

                    logs.AppendText("The client's username does not exists in the database, client is not accepted.\n");
                    newClient.Close();
                }
                else
                {
                    sendBoolean(true, newClient);       //give feedback to the client, connection is succeeded

                    clientInformation accepted_client = new clientInformation(user_name, newClient);
                    clientSockets.Add(accepted_client);
                    logs.AppendText("Client " + user_name + " is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(accepted_client));  //connection is successfull, start receiving messages
                    receiveThread.Start();
                }
            }
        }

        private void Receive(clientInformation thisClient)  //receive messages from cients
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];               //receive message from client                    
                    thisClient.clientSocket.Receive(buffer);
                    Console.WriteLine(Encoding.Default.GetString(buffer));

                    string operation = Encoding.Default.GetString(buffer);          //the client sends the name of the operation
                    operation = operation.Substring(0, operation.IndexOf("\0"));    //to be completed before sending actual data

                    if (operation == "post")
                    {
                        postMessage(thisClient);
                    }
                    else if (operation == "request")
                    {
                        sendRequested(thisClient);
                    }

                    else if (operation == "usernames")
                    {
                        sendUsernames(thisClient);
                    }

                    else if (operation == "follow")
                    {
                        sendFollow(thisClient);
                    }

                    else if (operation == "followedSweets")
                    {
                        sendFollowSweet(thisClient);
                    }

                    else if (operation == "usersSweets")
                    {
                        sendUserSweets(thisClient);
                    }
                    else if (operation == "deleteSweets")
                    {
                        deleteSweets(thisClient);
                    }
                    else if (operation == "sendFollowedUserNames")
                    {
                        sendFollowedUserNames(thisClient);
                    }
                    else if (operation == "sendUserNamesFollowingMe")
                    {
                        sendUserNamesFollowingMe(thisClient);
                    }
                    else if (operation == "blockUser")
                    {
                        blockUser(thisClient);
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("Client " + thisClient.userName + " has disconnected\n");
                    }
                    thisClient.clientSocket.Close();
                    clientSockets.Remove(thisClient);       //remove the client from the client list
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

        private void sendBoolean(bool value, Socket clientSocket) //send boolean value to the client
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            clientSocket.Send(buffer);
        }

        private void postMessage(clientInformation thisClient)
        {
            Byte[] buffer_3 = new Byte[64];               //receive sweet from client
            thisClient.clientSocket.Receive(buffer_3);

            string time_now = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            string incomingMessage = Encoding.Default.GetString(buffer_3);
            incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

            File.AppendAllText(@"sweets.txt", sweet_id + "\n" + thisClient.userName
                + "\n" + incomingMessage + "\n" + time_now
                + "\n\n");                                       //add sweet to the sweet file

            logs.AppendText("\nThe following sweet is posted\n");
            logs.AppendText("Sweet ID: " + sweet_id + "\n");
            logs.AppendText("Username: " + thisClient.userName + "\n");
            logs.AppendText("Message: " + incomingMessage + "\n");
            logs.AppendText("Time stamp: " + time_now + "\n\n");
            sweet_id++;
        }

        private void sendRequested(clientInformation thisClient)    //send all sweets that was not posted by that user
        {
            logs.AppendText("\nThe following sweets are sent to user " + thisClient.userName + ":\n");

            string fileName = @"sweets.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)        //we have at least 1 sweet already posted
                {
                    string[] lines = File.ReadAllLines(fileName);   

                    string sweet = "";
                    int i = 1;
                    bool willBeSent = false;

                    foreach (string line in lines)      //iterate through the sweet file
                    {
                        if (line != "")             //skip the empty lines
                        {
                            if (i % 4 == 2)         //sweets are written in 4 lines, that's why modula 4 is used
                            {
                                if (line != thisClient.userName)        //sweet's user name must not match with the user name of the client
                                {
                                    willBeSent = true;      //this sweet will be sent to the client
                                }
                            }
                            sweet += line;
                            sweet += "\n";

                            if (i % 4 == 0)
                            {
                                if (willBeSent)
                                {
                                    logs.AppendText(sweet + "\n");
                                    Byte[] buffer = Encoding.Default.GetBytes(sweet);
                                    thisClient.clientSocket.Send(buffer);                      //send sweets to client one by one
                                    Thread.Sleep(3);    //wait for a short time to give time for client to receive, before sending next data
                                }
                                willBeSent = false;
                                sweet = "";
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void sendUsernames(clientInformation thisClient)    //send all user names that exists in user name database
        {
            string fileName = "../../user-db.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    string[] lines = File.ReadAllLines(fileName);

                    foreach (string line in lines)
                    {
                        Byte[] buffer = Encoding.Default.GetBytes(line);
                        thisClient.clientSocket.Send(buffer);             // sending usernames to client one by one
                        Thread.Sleep(3);    //wait for a short time to give time for client to receive, before sending next data
                    }
                }
            }
        }

        private void sendFollow(clientInformation thisClient)
        {
            Byte[] buffer_4 = new Byte[64];               //receive user name to follow from client
            thisClient.clientSocket.Receive(buffer_4);
            string incomingUn = Encoding.Default.GetString(buffer_4);
            incomingUn = incomingUn.Substring(0, incomingUn.IndexOf("\0"));

            string fileName2 = "../../user-db.txt";
            string[] lines2 = File.ReadAllLines(fileName2);

            if (lines2.Contains(incomingUn) && incomingUn != thisClient.userName)     //this user name does not exist in the user database and not itself
            {
                bool check = false;
                string fileName = "../../followed.txt";

                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    string[] names = line.Split(' ');

                    if (names[0] == thisClient.userName && names[1] == incomingUn)  //this client already follows that user
                    {
                        check = true;
                        break;
                    }
                }

                if (!check)
                {
                    File.AppendAllText(fileName, thisClient.userName + " " + incomingUn + "\n");
                    string message = "You followed " + incomingUn;
                    Byte[] buffer = Encoding.Default.GetBytes(message);
                    thisClient.clientSocket.Send(buffer);
                    logs.AppendText(thisClient.userName + " followed " + incomingUn + "\n");
                }

                else
                {
                    string message = "You already follow this user!";
                    Byte[] buffer = Encoding.Default.GetBytes(message);
                    thisClient.clientSocket.Send(buffer);
                    logs.AppendText("User already follow this user!\n");
                }
            }

            else
            {
                string message = "Please check username that you want to follow!";
                Byte[] buffer = Encoding.Default.GetBytes(message);
                thisClient.clientSocket.Send(buffer);
                logs.AppendText("Wrong username address for following!\n");
            }
        }

        private bool followCheck(string followed, string user)
        {
            string fileName = "../../followed.txt";
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] names = line.Split(' ');
                if (names[0] == user && names[1] == followed)     //checking followed and user itself 
                {
                    return true;
                }
            }

            return false;
        }

        private void sendFollowSweet(clientInformation thisClient) //send sweets that were posted by users that this user follows
        {
            logs.AppendText("\nThe following sweets are sent to user " + thisClient.userName + ":\n");

            string fileName2 = "../../followed.txt";
            string fileName = @"sweets.txt";
            if (System.IO.File.Exists(fileName) && System.IO.File.Exists(fileName2))
            {
                if (new FileInfo(fileName).Length != 0 && new FileInfo(fileName2).Length != 0)
                {
                    string[] lines = File.ReadAllLines(fileName);

                    string sweet = "";
                    int i = 1;
                    bool willBeSent = false;

                    foreach (string line in lines)      //iterate through the sweet file
                    {
                        if (line != "")             //skip the empty lines
                        {
                            if (i % 4 == 2)         //sweets are written in 4 lines, that's why modula 4 is used
                            {
                                if (line != thisClient.userName && followCheck(line, thisClient.userName))  //sweet's user name must not match with the user name of the client and this user follows the user that posted this sweet
                                {
                                    willBeSent = true;      //this sweet will be sent to the client
                                }
                            }
                            sweet += line;
                            sweet += "\n";

                            if (i % 4 == 0)
                            {
                                if (willBeSent)
                                {
                                    logs.AppendText(sweet + "\n");
                                    Byte[] buffer = Encoding.Default.GetBytes(sweet);
                                    thisClient.clientSocket.Send(buffer);                      //send sweets to client one by one
                                    Thread.Sleep(3);
                                }
                                willBeSent = false;
                                sweet = "";
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void sendUserSweets(clientInformation thisClient)   //send the sweets posted by this user
        {
            logs.AppendText("\nThe following sweets are sent to user " + thisClient.userName + ":\n");

            string fileName = @"sweets.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    string[] lines = File.ReadAllLines(fileName);

                    string sweet = "";
                    int i = 1;
                    bool willBeSent = false;

                    foreach (string line in lines)      //iterate through the sweet file
                    {
                        if (line != "")             //skip the empty lines
                        {
                            if (i % 4 == 2)         //sweets are written in 4 lines, that's why modula 4 is used
                            {
                                if (line == thisClient.userName)        //sweet's user name must match with the user name of the client
                                {
                                    willBeSent = true;      //this sweet will be sent to the client
                                }
                            }
                            sweet += line;
                            sweet += "\n";

                            if (i % 4 == 0)
                            {
                                if (willBeSent)
                                {
                                    logs.AppendText(sweet + "\n");
                                    Byte[] buffer = Encoding.Default.GetBytes(sweet);
                                    thisClient.clientSocket.Send(buffer);                      //send sweets to client one by one
                                    Thread.Sleep(3);
                                }
                                willBeSent = false;
                                sweet = "";
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void deleteSweets(clientInformation thisClient) //delete sweets with given sweet IDs
        {
            Byte[] buffer_4 = new Byte[64];               //receive IDs from client
            thisClient.clientSocket.Receive(buffer_4);
            string incomingIDs = Encoding.Default.GetString(buffer_4);
            incomingIDs = incomingIDs.Substring(0, incomingIDs.IndexOf("\0"));

            string[] sweetIDs = incomingIDs.Split('-');     //IDs are sent as a string splitted by '-' 

            string fileName = @"sweets.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    string[] lines = File.ReadAllLines(fileName);

                    string sweetFile = "";      //will store content of the sweet file other than deleted sweets
                    string sweet = "";
                    int i = 1;
                    bool willBeDeleted = false;

                    foreach (string line in lines)      //iterate through the sweet file
                    {
                        if (i % 5 == 1)                 
                        {
                            if (sweetIDs.Contains(line))    //sweet's ID must match with one of the IDs that is wanted to be deleted
                            {
                                willBeDeleted = true;      //this sweet will be deleted
                            }
                        }
                        sweet += line;
                        sweet += "\n";

                        if (i % 5 == 0)
                        {
                            if (!willBeDeleted)     //the sweets that will be deleted are skipped
                            {
                                sweetFile += sweet;
                            }
                            willBeDeleted = false;
                            sweet = "";
                        }
                        i++;
                    }

                    File.WriteAllText(@"sweets.txt", sweetFile);    //write everything except the deleted sweets
                }
            }

            string messageToClient = "Successfully deleted Sweets with ID(s): " + incomingIDs;
            Byte[] buffer = Encoding.Default.GetBytes(messageToClient);
            thisClient.clientSocket.Send(buffer);             // sending success message to the client

            logs.AppendText("\n" + messageToClient + "\n");
        }

        private void sendFollowedUserNames(clientInformation thisClient)    //send the user names that this client follows
        {
            logs.AppendText("Sending user names that " + thisClient.userName + " follows to"+ "\n");

            string fileName = "../../user-db.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    string[] lines = File.ReadAllLines(fileName);

                    foreach (string line in lines)
                    {
                        if (followCheck(line, thisClient.userName))
                        {
                            Byte[] buffer = Encoding.Default.GetBytes(line);
                            thisClient.clientSocket.Send(buffer);             // sending usernames to client
                            Thread.Sleep(3);
                        }
                    }
                }
            }
        }

        private void sendUserNamesFollowingMe(clientInformation thisClient) //send the user names that follows this client
        {
            logs.AppendText("Sending user names that follow " + thisClient.userName + "\n");

            string fileName = "../../user-db.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    string[] lines = File.ReadAllLines(fileName);

                    foreach (string line in lines)
                    {
                        if (followCheck(thisClient.userName, line))
                        {
                            Byte[] buffer = Encoding.Default.GetBytes(line);
                            thisClient.clientSocket.Send(buffer);             // sending usernames to client
                            Thread.Sleep(3);
                        }
                    }
                }
            }
        }

        private void blockUser(clientInformation thisClient)
        {
            Byte[] buffer = new Byte[64];               //receive the user name to block from client
            thisClient.clientSocket.Receive(buffer);
            string incomingUserName = Encoding.Default.GetString(buffer);
            incomingUserName = incomingUserName.Substring(0, incomingUserName.IndexOf("\0"));

            string fileName = "../../blocks.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    
                }
                else 
                {

                }
            }
            else
            {

            }
        }
    }
}
