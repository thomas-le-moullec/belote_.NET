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
            NetworkComms.AppendGlobalIncomingPacketHandler<Greeting>("Greetings", this.PrintIncomingMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<Task>("WhichTasks", ReceiveAction);
        }

        public void DoActions(Task task)
        {
            // If Client.Task == GREETING || task == GETBOARD
            Console.WriteLine("Sending message to server saying '" + Client.Username + "'");
            NetworkComms.SendObject<string>("Greetings", Client.ServerIp, Client.ServerPort, Client.Username);
        }

        public void ReceiveAction(PacketHeader header, Connection connection, Task task)
        {
            DoActions(task);
        }

        public void PrintIncomingMessage(PacketHeader header, Connection connection, Greeting msg)
        {
            Console.WriteLine("-------------------------------" + msg.Message + " and id : " + msg.Id + "--------------------------");
        }
    }
}
