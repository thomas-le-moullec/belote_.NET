using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Model;

namespace ClientApplication
{
    class Client
    {

        //Variables server && username
        public Client()
        {
            //Request server IP and port number
            Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            string serverInfo = Console.ReadLine();

            //Parse the necessary information out of the provided string
            string serverIP = serverInfo.Split(':').First();
            int serverPort = int.Parse(serverInfo.Split(':').Last());

            //Keep a loopcounter
            NetworkComms.AppendGlobalIncomingPacketHandler<Greeting>("Greetings", this.PrintIncomingMessage);
            Console.WriteLine("Please enter your username:");
            string userName = Console.ReadLine();
            Console.WriteLine("Sending message to server saying '" + userName + "'");
            //Send the message in a single line
            NetworkComms.SendObject<string>("Greetings", serverIP, serverPort, userName);
            this.Run();
        }

        ~Client()
        {
            //We have used comms so we make sure to call shutdown
            NetworkComms.Shutdown();
        }

        public void Run()
        {
            while (true)
            {
                //Write some information to the console window
                // GETLINE AND SEND USER' ENTRY :

                //Check if user wants to go around the loop
                Console.WriteLine("\nPress q to quit or any other key to send another message.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
            }
        }

        public void PrintIncomingMessage(PacketHeader header, Connection connection, Greeting msg)
        {
            Console.WriteLine("-------------------------------" + msg.Message + " and id : " + msg.Id + "--------------------------");
        }

        static void Main(string[] args)
        {
            Client client = new Client();
        }
    }
}