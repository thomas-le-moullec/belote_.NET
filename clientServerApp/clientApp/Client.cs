using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using clientApp;

namespace ClientApplication
{
    public class Client
    {
        private Router router;
        private string serverIp;
        private int serverPort;
        private string username;

        //New Variable Player
        private Task work;
        private int id;
        private bool prompt;
        private int team;

        public Router Router
        {
            get { return router; }
            set { router = value; }
        }

        public Task Work
        {
            get { return work; }
            set { work = value; }
        }

        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
        }

        public bool Prompt
        {
            get { return prompt; }
            set { prompt = value; }
        }

        public int Team
        {
            get { return team; }
            set { team = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Client()
        {
            //Request server IP and port number
            Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            string serverInfo = Console.ReadLine();

            //Parse the necessary information out of the provided string
            ServerIp = serverInfo.Split(':').First();
            ServerPort = int.Parse(serverInfo.Split(':').Last());

            //Create Router to subscribe to channels and to communicate with the Server
            Router = new Router(this);
            Router.Subscribe();

            //Get UserName
            Console.WriteLine("Please enter your username:");
            Username = Console.ReadLine();

            //Set Prompt, this booleen will be use to display datas on user Screen. If it is false, it is User's turn.
            Prompt = true;

            //Set First Task
            Work = new Task();
            Work.Type = Task.TaskNature.GREETINGS;
            Router.DoActions(Work.Type);

            ScheduleTask sched = new ScheduleTask(this, 3000);
            sched.ScheduleAction();
            Run();
        }

        ~Client()
        {
        }

        public void Run()
        {
            while (true)
            {
                //if no infos display
                string entry = Console.ReadLine();

                //Check if user wants to go around the loop
                //Console.WriteLine("\nPress q to quit or any other key to send another message.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
            }
        }

        static void Main(string[] args)
        {
            Client client = new Client();
        }
    }
}