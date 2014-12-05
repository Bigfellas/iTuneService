﻿using System;
using System.Threading;
using System.Runtime;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;


namespace iTuneService
{
    class iTuneService : ServiceBase
    {
        Process p = null;
        string FileName;
        public iTuneService(String fn)
        {
            FileName = fn;

            this.ServiceName = "iTune Service";
            this.EventLog.Source = "iTune Service";
            this.EventLog.Log = "Application";

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            if (!EventLog.SourceExists("iTune Service"))
                EventLog.CreateEventSource("iTune Service", "Application");
        }

        protected override void OnStart(string[] args)
        {
            p = new Process();
            p.StartInfo.FileName = FileName;
            p.Start();
            Log.Write("iTune: Service Starting");
            Log.Write("Starting Process: " + FileName);
            Thread.Sleep(50);
            if (!p.HasExited)
            {
                Log.Write("Process: '" + FileName + "' has started with a pid of " + p.Id.ToString());
                Log.Write("iTune: Service Started");
            }
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            Log.Write("iTune: Service Stopping");
            Log.Write("Shutting down Process: '" + FileName + "', PID: " + p.Id.ToString());
            p.Kill();
            Thread.Sleep(50);
            if (p.HasExited)
            {
                Log.Write("Process: '" + FileName + "' has exited");
                Log.Write("iTune: Service Stopped");
            }

            base.OnStop();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
        }


        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }


        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }

        private void InitializeComponent()
        {

        }
    }

}
