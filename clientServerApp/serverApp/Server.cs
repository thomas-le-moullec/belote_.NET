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

        public Room Room
        {
            get { return room; }
            set { room = value; }
        }

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
            NetworkComms.AppendGlobalIncomingPacketHandler<String>("Greetings", this.InitialiseGame);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("WhichTasks", this.WhichTasks);
        }

        /// <summary>
        /// Get the provided message to the console window
        /// </summary>
        /// <param name="header">The packet header associated with the incoming message</param>
        /// <param name="connection">The connection used by the incoming message</param>
        /// <param name="greeting">The message to be printed to the console</param>
        public void InitialiseGame(PacketHeader header, Connection connection, String greeting)
        {
            // Select last Room or add another one if the number player of the last one is >= 4
            List<Player> players = Room.Players;
            Player newPlayer = new Player();
            int idPlayer = players.Count + 1;
            Greeting welcome = new Greeting();

            //Add player in Players list of the room
            players.Add(newPlayer);
            newPlayer.Id = idPlayer;
            newPlayer.Team = (players.Count + 1) % 2;
            newPlayer.TaskState.Type = Task.TaskNature.WAIT;

            //Start of the Game
            if (idPlayer == 4)
            {
                Console.WriteLine("WE WILL START THE GAME !\n");
                foreach (var player in players)
                {
                    //distribution
                    player.TaskState.Type = Task.TaskNature.GET_HAND;
                }
            }

            //Fill all the datas the new player needs to know
            welcome.Id = idPlayer;
            welcome.Connection = true;
            welcome.Message = "Hello " + greeting;
            welcome.Team = players[players.Count - 1].Team;

            connection.SendObject("Greetings", welcome);
        }

        public void WhichTasks(PacketHeader header, Connection connection, int id)
        {
            connection.SendObject("WhichTasks", Room.Players[id - 1].TaskState.Type);
        }

        static void Main(string[] args)
        {
            Server server = new Server();
        }
    }
}