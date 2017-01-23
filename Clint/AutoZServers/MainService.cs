//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace AutoZServers
{
    public partial class MainService : ServiceBase
    {
        private Process proc = new Process();
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                this.proc.StartInfo.FileName = args[0].ToString();
                this.proc.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        protected override void OnStop()
        {
            try
            {
                this.proc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
