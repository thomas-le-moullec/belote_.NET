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
                NetworkComms.SendObject<int>("WhichTasks", Client.ServerIp, Client.ServerPort, Client.Id);
            }
        }

        public void ReceiveAction(PacketHeader header, Connection connection, Model.Task task)
        {
            DoActions(task.Type);
        }

        public void Greeting(PacketHeader header, Connection connection, Greeting greeting)
        {
            if (greeting.Connection == false) {
                System.Environment.Exit(1);
            }
            Client.Id = greeting.Id;
            Console.WriteLine(greeting.Message + " , your will play with id :"+greeting.Id);
        }
    }
}
