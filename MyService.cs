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
            //LogClass logstart = new LogClass();
            EventLogger.Log("Service Started");
            Listener listener = new Listener(); //Object of Listener Class.
            listener.StartListener(); //Calls the method StartListener.
           
        }

        protected override void OnStop()
        {
           // LogClass logend = new LogClass();
            EventLogger.Log("Service Ended");
            Listener listener = new Listener();
            listener.server.Stop();
        }
    }
}
