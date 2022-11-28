/*
  FILE			: PlayAgainWindow.xaml.cs
  PROJECT		: A06 - Services
  PROGRAMMER(S)	: Raj Dudhat, Jainish Patel, Philip Wojdyna
  FIRST VERSION	: 2022-11-25
  DESCRIPTION	: XAML CS file "PlayAgainWindow" for Hi-Lo Game created using WPF and TCP/IP methods. 
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
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace Client
{
   
    public partial class PlayAgainWindow : Window
    {
        public PlayAgainWindow()
        {
            InitializeComponent();
        }



        /*
        FUNCTION : ExitButtonYouWinWindow_Click
        DESCRIPTION : Exits Application On Click.
        PARAMETERS : object sender, RoutedEventArgs e
        RETURNS : return void
        */
        private void ExitButtonYouWinWindow_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }



        /*
        FUNCTION : PlayAgainButton_Click
        DESCRIPTION : Restarts the Application so user is able to play again.
        PARAMETERS : object sender, RoutedEventArgs e
        RETURNS : return void
        */
        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {

            ClientWindow obj = new ClientWindow();

            obj.Show();

            this.Close();

        }
    }
}
