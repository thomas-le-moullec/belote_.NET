using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Model;

namespace ClientApplication
{
    public class Router
    {
        private Client client;

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public Router ()
        {
        }

        ~Router()
        {
            NetworkComms.Shutdown();
        }

        public Router(Client client)
        {
            Client = client;
        }

        public void Subscribe()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<Greeting>("Greetings", Greeting);
            NetworkComms.AppendGlobalIncomingPacketHandler<Model.Task>("WhichTasks", ReceiveAction);
            NetworkComms.AppendGlobalIncomingPacketHandler<List<Model.Card>>("GetHands", GetHand);
            NetworkComms.AppendGlobalIncomingPacketHandler<Model.Card>("GetTrumps", GetTrump);
        }

        public void DoActions(Model.Task.TaskNature task)
        {
            if (task == Model.Task.TaskNature.GREETINGS)
            {
                Console.WriteLine("Sending message to server saying '" + Client.Username + "'");
                NetworkComms.SendObject<string>("Greetings", Client.ServerIp, Client.ServerPort, Client.Username);
            }

            if (task == Model.Task.TaskNature.ASKFORTASK)
            {
                Console.WriteLine("Ask for Task");
                NetworkComms.SendObject<int>("WhichTasks", Client.ServerIp, Client.ServerPort, Client.Player.Id);
            }

            if (task == Model.Task.TaskNature.GET_HAND)
            {
                Console.WriteLine("Client will get the HAND");
                NetworkComms.SendObject<int>("GetHands", Client.ServerIp, Client.ServerPort, Client.Player.Id);
            }

            if (task == Model.Task.TaskNature.WAIT)
            {
                //Display status of the game
            }
        }

        public void ReceiveAction(PacketHeader header, Connection connection, Model.Task task)
        {
            Console.WriteLine("Receive action type :"+task.Type);
            DoActions(task.Type);
        }

        public void GetHand(PacketHeader header, Connection connection, List<Model.Card> cards)
        {
            Client.Player.Hand = cards;
            //display Hand of player
        }

        public void GetTrump(PacketHeader header, Connection connection, Model.Card trump)
        {
            //Display Trump and ask to get it
        }

        public void Greeting(PacketHeader header, Connection connection, Greeting greeting)
        {
            //Check if connection is OK
            if (greeting.Connection == false) {
                System.Environment.Exit(1);
            }

            //Get Data from the HandShake
            Client.Player.Id = greeting.Id;
            Client.Player.Team = greeting.Team;
            Console.WriteLine(greeting.Message + " , your will play with id :"+greeting.Id+" and are in the TEAM"+greeting.Team);
        }
    }
}
