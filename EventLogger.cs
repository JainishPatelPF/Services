using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_A06
{
    public static class EventLogger
    {
        public static void Log(string message)
        {
            EventLog serviceEventLog = new EventLog();
            if (!EventLog.SourceExists("D:\\Temp"))
            {
                EventLog.CreateEventSource("D:\\Temp", "Eventlog.txt");
            }
            serviceEventLog.Source = "D:\\Temp";
            serviceEventLog.Log = "Eventlog.txt";
            serviceEventLog.WriteEntry(message);
        }

    }

}
