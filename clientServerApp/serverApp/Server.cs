using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Model;

namespace ServerApplication
{
    class Server
    {
        private Room room;//will become a pool of rooms (LIST)

        public Server()
        {
            //initialise Room
            room = new Room();
            room.Id = 1;

            //create endPoints to communicate with clients
            createEndPoints();

            //Start listening for incoming connections
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0));

            //Print out the IPs and ports we are now listening on
            Console.WriteLine("Server listening for TCP connection on:");
            foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);

            //Let the user close the server
            Console.WriteLine("\nPress any key to close server.");
            Console.ReadKey(true);
        }

        ~ Server()
        {
            //We have used NetworkComms so we should ensure that we correctly call shutdown
            NetworkComms.Shutdown();
        }

        public void createEndPoints()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<String>("Greetings", this.GetIncomingMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("WhichTask", this.WhichTasks);
        }

        /// <summary>
        /// Get the provided message to the console window
        /// </summary>
        /// <param name="header">The packet header associated with the incoming message</param>
        /// <param name="connection">The connection used by the incoming message</param>
        /// <param name="greeting">The message to be printed to the console</param>
        public void GetIncomingMessage(PacketHeader header, Connection connection, String greeting)
        {
            //add player in room or add Room and add player in Room
            Console.WriteLine("\nIN SERVER, THE ROOM : " + room.TrumpTaker);
            Greeting welcome = new Greeting();
            welcome.Id = 0;//Room.players.size() + 1;
            welcome.Connection = true;
            welcome.Message = "Hello " + greeting;
            Console.WriteLine("\nA message was received from " + connection.ToString() + " which said '" + greeting + "'.");
            connection.SendObject("Greetings", welcome);
        }

        public void WhichTasks(PacketHeader header, Connection connection, int id)
        {
            connection.SendObject("WhichTasks", "");
        }

        static void Main(string[] args)
        {
            Server server = new Server();
        }
    }
}