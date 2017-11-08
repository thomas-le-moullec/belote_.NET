using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace ClientApplication
{
    public class Client
    {
        private Router router;
        private string serverIp;
        private int serverPort;
        private string username;
        private Task task;

        public Router Router
        {
            get { return router; }
            set { router = value; }
        }

        public Task task
        {
            get { return task; }
            set { task = value; }
        }

        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
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

            //Get UserName
            Console.WriteLine("Please enter your username:");
            Username = Console.ReadLine();

            //Set First Task
            Task = new Task();
            Router.DoActions(Task);
            Run();
        }

        ~Client()
        {
        }

        public void Run()
        {
            while (true)
            {
                //Write some information to the console window
                // GETLINE AND SEND USER' ENTRY :
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