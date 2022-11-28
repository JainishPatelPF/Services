﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_A06
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
