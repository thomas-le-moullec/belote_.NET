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
        private bool prompt;
        private Player player;
        private Board board;
        private Card putCard;

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public Card PutCard
        {
            get { return putCard; }
            set { putCard = value; }
        }

        public Board Board
        {
            get { return board; }
            set { board = value; }
        }

        public Router Router
        {
            get { return router; }
            set { router = value; }
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

        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
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

            Board = new Board();

            Player = new Player();
            //Get UserName
            Console.WriteLine("Please enter your username:");
            Player.Username = Console.ReadLine();

            //Set Prompt, this booleen will be use to display datas on user Screen. If it is true, it is User's turn.
            Prompt = false;
            //Set First Task
            Player.TaskState.Type = Task.TaskNature.GREETINGS;
            try
            {
                Router.DoActions(Player.TaskState.Type);
            }
            catch
            {
                System.Environment.Exit(1);
            }

            ScheduleTask sched = new ScheduleTask(this, 1000);
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

                //refresh d'information.

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