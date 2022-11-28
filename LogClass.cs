using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side___A05
{
   
    internal class LogClass
    {
        public object log1 = new object();

        internal void Log(string message)
        {
            lock (log1)
            {
                StreamWriter streamWriter = File.AppendText("D:\\Temp\\log.txt");
                streamWriter.WriteLine(DateTime.Now.ToString() + ":" + message);
                streamWriter.Close();
            }
        }

    }
}
