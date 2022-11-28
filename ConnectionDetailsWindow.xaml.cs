/*
  FILE			: ConnectionDetailsWindow.xaml.cs
  PROJECT		: A06 - Services
  PROGRAMMER(S)	: Raj Dudhat, Jainish Patel, Philip Wojdyna
  FIRST VERSION	: 2022-11-25
  DESCRIPTION	: XAML CS file "ConnectionDetailsWindow" for Hi-Lo Game created using WPF and TCP/IP methods. 
*/
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Client
{
    
    public partial class ConnectionDetailsWindow : Window
    {
        public ConnectionDetailsWindow()
        {
            InitializeComponent();
        }



        /*
        FUNCTION : ExitBtn_Click
        DESCRIPTION : Exits application on click.
        PARAMETERS : object sender, RoutedEventArgs e
        RETURNS : return void
        */
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }



        private void UsernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
           //Unused
        }



        /*
        FUNCTION : SubmitBtn_Click
        DESCRIPTION : New ClientWindow object is created and connection details are communicated with server.
        PARAMETERS : object sender, RoutedEventArgs e
        RETURNS : return void
        */
        public void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {

            ClientWindow obj = new ClientWindow();  //showing the other window

            obj.Show();

            this.Close(); //closing the window

            //file paths for both ip and port files
            string ipPath = "D:\\Temp\\ippath.txt";
            string portPath = "D:\\Temp\\portpath.txt";

            //using stream writer to write in file
            using (StreamWriter streamWriter = new StreamWriter(ipPath))
            {
                streamWriter.WriteLine(ipAddressTextbox.Text);

            }

            //using stream writer to write in file
            using (StreamWriter streamWriter = new StreamWriter(portPath))
            {

                streamWriter.WriteLine(portTextbox.Text);
            }
        }

    }
}
