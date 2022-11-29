/*
  FILE			: Listener.cs
  PROJECT		: A05 – TCP/IP
  PROGRAMMER(S)	: Raj Dudhat, Jainish Patel, Philip Wojdyna
  FIRST VERSION	: 2022-11-28
  DESCRIPTION	: Server Side for the Hi-Lo Game.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Dynamic;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using Server_Side___A05;

namespace Service_A06
{
    public class Listener
    {
        int rand; //stores the random number.
        public TcpListener server; //listener variable
        public void StartListener()
        {
           
            try
            {
                LogClass test = new LogClass();
                 
                // Set the TcpListener on port from config file.
                //string portTemp = ConfigurationManager.AppSettings.Get("port");
                Int32 port = 13000;//Convert.ToInt32(portTemp);

                //  IPAddress localAddr = IPAddress.Parse("10.192.119.145"); //Server's IP Address.

                //setting the ip from config file
                string myIpTemp = "10.192.223.192";//ConfigurationManager.AppSettings.Get("Ip_Add");
                IPAddress myIp = IPAddress.Parse(myIpTemp);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(myIp, port);

                // Start listening for client requests.
                server.Start();
                LogClass logClass1 = new LogClass();
                logClass1.Log("Server Started");

                Random r = new Random();
                int min = r.Next(1, 50);
                int max = r.Next(52, 100);

                string path = "D:\\Temp\\Config.txt";
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.WriteLine(min);
                    streamWriter.WriteLine(max);
                }

                rand = randomNum();
                
                // Enter the listening loop.
                while (true)
                {
                    logClass1.Log("Waiting for a connection... \n");
                    
                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    test.Log("Client Connected");
                    logClass1.Log("Connected!");
                    ParameterizedThreadStart ts = new ParameterizedThreadStart(Worker);
                    Thread clientThread = new Thread(ts);
                    clientThread.Start(client);
                    test.Log("Thread Started");

                }
            }
            catch (SocketException e)
            {
                
                LogClass logException = new LogClass(); //logging exception to the file
                logException.Log(e.Message.ToString());
            }
            finally
            {

                // Stop listening for new clients.
                
                LogClass logEnd = new LogClass();
                server.Stop();
                logEnd.Log("Server Ended");
            }
          
            

        }

        /* FUNCTION		: Worker()
        * DESCRIPTION   : Works to check the guess number.
        * PARAMETERS    : object
        * RETURNS		: void
        */
        public void Worker(Object o)
        {
            TcpClient client = (TcpClient)o;    //client variable.
            Byte[] bytes = new Byte[256]; // Buffer for reading data
            String data = null; //stores the incoming message from server as a string.
            bool closeClient = true; //makes sure it does not closes two times.
            int i;
            LogClass logWrite = new LogClass();
            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                if(data == "Ending") //If the Message from the incoming server says that its ending it closes gracefully.
                {
                    closeClient = false;
                    client.Close();//shuts down and ends the connection.
                    logWrite.Log("\n Disconnected.");
                    break;
                }
                else //else it will go to the main game.
                {
                    int myData = Convert.ToInt32(data); //converting it into int.
                    Game(myData, rand, client); //calls the Game Function to check the number.
                }
                
            }

            if(closeClient == true)
            {
                // Shutdown and end connection
                client.Close();
            }
            
        }

        /* FUNCTION		: randomNum()
        * DESCRIPTION   : Generates the random Number.
        * PARAMETERS    : None
        * RETURNS		: int
        */
        int randomNum()
        {
            StreamReader sr = new StreamReader("D:\\Temp\\Config.txt"); //reads the config file.
            string[] arr = new string[5]; 
            Random random = new Random(); //Random object of class Random.

            int i = 0;
            while (sr.Peek() != -1) //Peeks until its end of line.
            {
                arr[i] = sr.ReadLine(); //Stores the line in the array.

                i++;
            }
            int min = Convert.ToInt32(arr[0]);  //Min in 0th Element
            int max = Convert.ToInt32(arr[1]); //max in 1st Element
            int rand = random.Next(min, max);   //Generates random number.
            return rand;    //Returns Random Number.

        }

        /* FUNCTION		: Game()
        * DESCRIPTION   : Actual Game Logic.
        * PARAMETERS    : int, int, TcpClient
        * RETURNS		: void
        */
        void Game(int data, int raNUm, TcpClient client)
        {
            byte[] bytes = new byte[256]; //stores it in ASCII.
            string message = ""; //Stores the message.
            if(data == raNUm) //Checks if its correct.
            {
                message = "Correct";
                bytes = System.Text.Encoding.ASCII.GetBytes(message); //Converts into ASCII.
                NetworkStream stream2 = client.GetStream(); //Creates a new NetworkStream.
                stream2.Write(bytes, 0, bytes.Length);  //writes the data into the stream.

            }
            else if(data < raNUm) //Checks if its Low.
            {
                message = "Too Low";
                bytes = System.Text.Encoding.ASCII.GetBytes(message); //Converts into ASCII.
                NetworkStream stream2 = client.GetStream(); //Creates a new NetworkStream.
                stream2.Write(bytes, 0, bytes.Length);  //writes the data into the stream.
            }
            else if(data > raNUm) //Checks if its High.
            {
                message = "Too High";
                bytes = System.Text.Encoding.ASCII.GetBytes(message); //Converts into ASCII.
                NetworkStream stream2 = client.GetStream(); //Creates a new NetworkStream.
                stream2.Write(bytes, 0, bytes.Length);  //writes the data into the stream.
            }
        }

 


    }



    
}
