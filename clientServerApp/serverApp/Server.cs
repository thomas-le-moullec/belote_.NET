using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Model;
using serverApp;

namespace ServerApplication
{
    class Server
    {
        private Room room;

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
            CreateEndPoints();

            //Start listening for incoming connections
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, 8080));
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

        /// <summary>   Creates end points. </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>

        public void CreateEndPoints()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<String>("Greetings", this.InitialiseGame);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("WhichTasks", this.WhichTasks);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("GetHands", this.GetHands);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("GetTrumps", this.GetTrump);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("GetBoards", this.GetBoard);
            NetworkComms.AppendGlobalIncomingPacketHandler<Response>("Responses", this.GetResponses);
            NetworkComms.AppendGlobalIncomingPacketHandler<Card>("PutCards", this.PutCard);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("GetScores", this.GetScores);
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
            newPlayer.Username = greeting;

            //Start of the Game
            if (idPlayer == 4)
            {
                Console.WriteLine("WE WILL START THE GAME !\n");
                Services.TrumpGeneration(Room.RoomBoard);
                Console.WriteLine("TRUMP Value at the initialisation TYPE:"+Room.RoomBoard.Trump.Type+" VALUE :"+Room.RoomBoard.Trump.Val+"\n");
                Services.SetCardPoints(Room);//a vérifier
                foreach (var player in players)
                {
                    Services.DistribCards(player, Room.RoomBoard.Pick, 5);
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

        /// <summary>   Tells the client to ask for task </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The packet header associated with the incoming message. </param>
        /// <param name="connection">   The connection used by the incoming message. </param>
        /// <param name="id">           The identifier. </param>

        public void WhichTasks(PacketHeader header, Connection connection, int id)
        {
            Console.WriteLine("WHICH TASK!\n");
            connection.SendObject("WhichTasks", Room.Players[id - 1].TaskState);
        }

        /// <summary>   Tells the client to display the board </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The packet header associated with the incoming message. </param>
        /// <param name="connection">   The connection used by the incoming message. </param>
        /// <param name="id">           The identifier. </param>

        public void GetBoard(PacketHeader header, Connection connection, int id)
        {
            Console.WriteLine("GET Board!\n");
            connection.SendObject("GetBoards", Room.RoomBoard);
        }

        /// <summary>   Tells the client to display the scores </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The packet header associated with the incoming message. </param>
        /// <param name="connection">   The connection used by the incoming message. </param>
        /// <param name="id">           The identifier. </param>

        public void GetScores(PacketHeader header, Connection connection, int id)
        {
            Console.WriteLine("GET Scores!\n");
            connection.SendObject("GetScores", Room.ScoreBoard);
            Room.Players[id - 1].TaskState.Type = Task.TaskNature.WAIT;
            if (Room.Players[id - 1].Hand.Count != 0)
            {
                if (id == Room.RoomBoard.IdTurn)
                {
                    Room.Players[id - 1].TaskState.Type = Task.TaskNature.PUT_CARD;
                }
            }
        }

        /// <summary>   Tell the client to display the player's hand </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The packet header associated with the incoming message. </param>
        /// <param name="connection">   The connection used by the incoming message. </param>
        /// <param name="id">           The identifier. </param>

        public void GetHands(PacketHeader header, Connection connection, int id)
        {
            Console.WriteLine("-------- GET IN THE GETHANDS HANDLER --------");
            connection.SendObject("GetHands", Room.Players[id - 1].Hand);
            Console.WriteLine("BEFORE GET HAND id:" + id + " will be " + Room.Players[id - 1].TaskState.Type + "!\n");
            Room.Players[id - 1].TaskState.Type = Task.TaskNature.WAIT;
            if (Room.RoomBoard.IdTurn == -1 && id == 1)
            {
                Room.Players[0].TaskState.Type = Task.TaskNature.GET_TRUMP;
                Room.RoomBoard.IdTurn = 1;
            }
            if (id == Room.RoomBoard.IdTurn && Room.Players[Room.RoomBoard.IdTurn - 1].Hand.Count > 5)
            {
                Room.Players[Room.RoomBoard.IdTurn - 1].TaskState.Type = Task.TaskNature.PUT_CARD;
            }
            Console.WriteLine("AFTER GET HAND id:" + id + " will be " + Room.Players[id - 1].TaskState.Type + "!\n");
        }

        /// <summary>   Tells the client to diplay the trump </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The packet header associated with the incoming message. </param>
        /// <param name="connection">   The connection used by the incoming message. </param>
        /// <param name="id">           The identifier. </param>

        public void GetTrump(PacketHeader header, Connection connection, int id)
        {
            Console.WriteLine("GET TRUMP!\n");
            Console.WriteLine("TRUMP Value IN GET TRUMP TYPE:" + Room.RoomBoard.Trump.Type + " VALUE :" + Room.RoomBoard.Trump.Val + "\n");
            connection.SendObject("GetTrumps", Room.RoomBoard.Trump);
        }

        /// <summary>   Gets the responses to the question : "do you want the trump ?" </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The packet header associated with the incoming message. </param>
        /// <param name="connection">   The connection used by the incoming message. </param>
        /// <param name="response">     The response. </param>

        public void GetResponses(PacketHeader header, Connection connection, Response response)
        {
            Console.WriteLine("Response From player :"+response.Id+" and text: "+response.Text+"\n");
            //Response for GET TRUMP
            if (Room.Players[response.Id - 1].TaskState.Type == Task.TaskNature.GET_TRUMP)
            {
                // Player accepted the trump
                Console.WriteLine("GET TRUMP SERVER RESPONSE\n");
                if (string.Equals(response.Text, "Y", StringComparison.CurrentCultureIgnoreCase) || string.Equals(response.Text, "Yes", StringComparison.CurrentCultureIgnoreCase))
                {
                    //distribute Cards.
                    Console.WriteLine("DETECTED POSITIVE RESPONSE\n");
                    foreach (var player in Room.Players)
                    {
                        if (player.Id.Equals(response.Id) == false)
                        {
                            Services.DistribCards(player, Room.RoomBoard.Pick, 3);
                        }
                        else
                        {
                            //Distribute trump
                            Services.DistribTrump(player, Room.RoomBoard);
                            Services.DistribCards(player, Room.RoomBoard.Pick, 2);
                            Room.TrumpTaker = player.Id;
                        }
                        player.TaskState.Type = Task.TaskNature.GET_HAND;
                    }
                    Services.SetCardPoints(Room);
                }
                else
                {
                    Room.RoomBoard.IdTurn = response.Id % 4 + 1;
                    Room.Players[response.Id - 1].TaskState.Type = Task.TaskNature.WAIT;
                    Room.Players[Room.RoomBoard.IdTurn - 1].TaskState.Type = Task.TaskNature.GET_TRUMP;
                }
            }
        }

        /// <summary>   Puts a card from the player's hand to the fold </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The packet header associated with the incoming message. </param>
        /// <param name="connection">   The connection used by the incoming message. </param>
        /// <param name="card">         The card. </param>

        public void PutCard(PacketHeader header, Connection connection, Card card)
        {
            int id = card.IdPlayer;
            int nextPlayer = (card.IdPlayer % 4) + 1;
            Player player = Room.Players[id - 1];
            //Vérification à faire ici et non côté client.
            Services.PutCard(player, Room.RoomBoard, card);
            if (Room.RoomBoard.Fold.Count < 4)
            {
                Console.WriteLine("FOLD NOT FULL!\n");
                player.TaskState.Type = Task.TaskNature.WAIT;
            }
            else
            {
                Console.WriteLine("WE HAVE TO DETERMINE THE WINNER!\n");
                nextPlayer = Services.WinFold(Room.RoomBoard.Fold, room.RoomBoard.Trump.GetType());
                Room.ScoreBoard.ScoreTeams[0] = Room.Players[0].Score + Room.Players[2].Score;
                Room.ScoreBoard.ScoreTeams[1] = Room.Players[1].Score + Room.Players[3].Score;
                Services.SetScores(Room.RoomBoard.Fold, Room.Players);
                Room.ScoreBoard.IdWinnerFold = nextPlayer;
                foreach (var bestCard in Room.RoomBoard.Fold)
                {
                    if (bestCard.IdPlayer == nextPlayer)
                    {
                        Room.ScoreBoard.CardWinnerFold = bestCard;
                    }
                }
                Room.RoomBoard.Fold.Clear();
            }
            Room.Players[nextPlayer - 1].TaskState.Type = Task.TaskNature.PUT_CARD;
            Room.RoomBoard.IdTurn = nextPlayer;
            if (Room.RoomBoard.Fold.Count == 0)
            {
                foreach (var gamer in Room.Players)
                {
                    gamer.TaskState.Type = Task.TaskNature.GET_SCORES;
                }
            }
            connection.SendObject("PutCards", true);
        }

        static void Main(string[] args)
        {
            Server server = new Server();
        }
    }
}