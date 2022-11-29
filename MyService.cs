/*
  FILE			: MyService.cs
  PROJECT		: A06 
  PROGRAMMER(S)	: Raj Dudhat, Jainish Patel, Philip Wojdyna
  FIRST VERSION	: 2022-11-28
  DESCRIPTION	: Service file  for the Hi-Lo Game.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace Service_A06
{
    public partial class MyService : ServiceBase
    {
        public MyService()
        {
            InitializeComponent();

        }

        protected override void OnStart(string[] args)
        {
            //logging for start of the server
            LogClass logStart = new LogClass();
            logStart.Log("Service Started");
            Listener listener = new Listener(); //Object of Listener Class.
            listener.StartListener(); //Calls the method StartListener.
           
        }

        protected override void OnStop()
        {
           LogClass logEnd = new LogClass();
            logEnd.Log("Service Ended"); //logging the end of the server
            Listener listener = new Listener();
            listener.server.Stop(); //stoping the server
        }
    }
}
