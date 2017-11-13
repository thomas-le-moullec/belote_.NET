using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApplication;
using System.Timers;

namespace clientApp
{
    public class ScheduleTask
    {
        Client client;
        int time;

        public ScheduleTask(Client client, int t)
        {
            Client = client;
            Time = t;
        }

        /// <summary>   Calls 'DoAction' every 'time' seconds, asking for a task</summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>

        public void ScheduleAction()
        {
            Timer timer = new Timer();
            timer.Interval = Time;

            // Hook up the Elapsed event for the timer.
            timer.Elapsed += EventTimeHandler;

            // Have the timer fire repeated events (true is the default)
            timer.AutoReset = true;

            // Start the timer
            timer.Enabled = true;
        }

        /// <summary>   Handler, called when the event time. </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="source">   Source. </param>
        /// <param name="e">        Elapsed event information. </param>

        public void EventTimeHandler(Object source, ElapsedEventArgs e)
        {
            if (Client.Prompt == false)
            {
                Client.Router.DoActions(Model.Task.TaskNature.ASKFORTASK);
            }
            //Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
        public Client Client
        {
            get { return client; }
            set { client = value; }
        }
        public int Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}