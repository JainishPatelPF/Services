/*
  FILE			: ClientWindow.xaml.cs
  PROJECT		: A06 - Services
  PROGRAMMER(S)	: Raj Dudhat, Jainish Patel, Philip Wojdyna
  FIRST VERSION	: 2022-11-25
  DESCRIPTION	: XAML CS file "ClientWindow" for Hi-Lo Game created using WPF and TCP/IP methods. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Markup;
using System.Configuration;

namespace Client
{
    
    public partial class ClientWindow : Window
    {

        public ClientWindow()
        {
            InitializeComponent();
        }



        /*
        FUNCTION : MakeGuessBtn_Click
        DESCRIPTION : Sends Guess to Server for Validation
        PARAMETERS : object sender, RoutedEventArgs e
        RETURNS : return void
        */
        public void MakeGuessBtn_Click(object sender, RoutedEventArgs e)
        {
            //Insert text file paths into string
            string ipPath = "D:\\Temp\\ippath.txt";
            string portPath = "D:\\Temp\\portpath.txt";

            //Read text files and extract strings
            StreamReader sr = new StreamReader(ipPath);
            StreamReader sr2 = new StreamReader(portPath);
            string[] arr = new string[5];
            string[] arr2 = new string[5];

            
            int i = 0;
            //parsing the data from files
            while (sr.Peek() != -1)
            {
                arr[i] = sr.ReadLine();

                i++;
            }
            i = 0;
            //parsing the data from files
            while (sr2.Peek() != -1)
            {
                arr2[i] = sr2.ReadLine();

                i++;
            }
            //ip and port stored in string
            string ip = arr[0]; 
            string port = arr2[0];

            //Guess stored in string
            string Guess = GuessTextBox.Text;
            //Port converted to int and stored as int
            int myPort = Convert.ToInt32(port);

            //Send values to connect client function.
            ConnectClient(ip, Guess, myPort);

        }



        /*
        FUNCTION : ConnectClient
        DESCRIPTION : Recieves server information to allow the Client to connect with the proper details.
        PARAMETERS : String server, String message, int port
        RETURNS : return void
        */
        private void ConnectClient(String server, String message, int port)
        {

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.

                //Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                //If the string recieved from the server is "Correct", open the "you win" window.
                if(responseData == "Correct")
                {

                    //Create PlayAgainWindow Object and Display while closing current window.
                    PlayAgainWindow obj = new PlayAgainWindow();

                    obj.Show(); //Shows new object

                    this.Close(); //Closes current object 

                } 
                //If the string recieved from the server is "Too High", Changes the empty status label to display "Too High".
                else if(responseData == "Too High")
                {
                    
                    //Change label content to ""
                    StatusLabel.Content = "Too High";

                }
                //If the string recieved from the server is "Too Low", Changes the empty status label to display "Too Low".
                else if (responseData == "Too Low")
                {

                    //Change label content to ""
                    StatusLabel.Content = "Too Low";

                }

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }



        private void GuessTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Unused
        }



        /*
        FUNCTION : ExitBtn_Click
        DESCRIPTION : Exits the application on click.
        PARAMETERS : object sender, RoutedEventArgs e
        RETURNS : return void
        */
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            //Shutdown Application
            System.Windows.Application.Current.Shutdown();
        }
    }
}
