using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Model;
using clientApp;
using NetworkCommsDotNet.Tools;

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

        /// <summary>   Subscribe to different channels </summary>
        /// <remarks>   , 13/11/2017. </remarks>

        public void Subscribe()
        {
            //Subscribe to Channels.
            NetworkComms.AppendGlobalIncomingPacketHandler<Greeting>("Greetings", Greeting);
            NetworkComms.AppendGlobalIncomingPacketHandler<Model.Task>("WhichTasks", ReceiveAction);
            NetworkComms.AppendGlobalIncomingPacketHandler<List<Model.Card>>("GetHands", GetHand);
            NetworkComms.AppendGlobalIncomingPacketHandler<Model.Card>("GetTrumps", GetTrump);
            NetworkComms.AppendGlobalIncomingPacketHandler<Model.Board>("GetBoards", GetBoard);
            NetworkComms.AppendGlobalIncomingPacketHandler<Boolean>("PutCards", PutCard);
            NetworkComms.AppendGlobalIncomingPacketHandler<Model.ScoreBoard>("GetScores", GetScores);
        }

        /// <summary>   Executes operation depending on the attributed task </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="task"> The task. </param>

        public void DoActions(Model.Task.TaskNature task)
        {
            if (task == Model.Task.TaskNature.GREETINGS)
            {
                //Console.WriteLine("Sending message to server saying '" + Client.Player.Username + "'");
                try
                {
                    NetworkComms.SendObject<string>("Greetings", Client.ServerIp, Client.ServerPort, Client.Player.Username);
                }
                catch
                {
                    throw new Exception();
                }
            }

            if (task == Model.Task.TaskNature.ASKFORTASK)
            {
            //    Console.WriteLine("Ask for Task");
                NetworkComms.SendObject<int>("WhichTasks", Client.ServerIp, Client.ServerPort, Client.Player.Id);
            }

            if (task == Model.Task.TaskNature.GET_HAND)
            {
              //  Console.WriteLine("Client will get the HAND");
                NetworkComms.SendObject<int>("GetHands", Client.ServerIp, Client.ServerPort, Client.Player.Id);
            }

            if (task == Model.Task.TaskNature.GET_TRUMP)
            {
                //Console.WriteLine("Client will get the TRUMP");
                NetworkComms.SendObject<int>("GetTrumps", Client.ServerIp, Client.ServerPort, Client.Player.Id);
            }

            if (task == Model.Task.TaskNature.GET_BOARD)
            {
                //Console.WriteLine("Client will get the BOARD");
                NetworkComms.SendObject<int>("GetBoards", Client.ServerIp, Client.ServerPort, Client.Player.Id);
            }

            if (task == Model.Task.TaskNature.PUT_CARD)
            {
                //Console.WriteLine("Client will PUT a CARD");
                DoActions(Model.Task.TaskNature.GET_BOARD);
                Client.PutCard = Services.PutCard(Client);
                Console.WriteLine("Please Choose which card do you want to play with this format : [TYPE]-[VALUE] (e.g CLUB:Q) :");
                NetworkComms.SendObject<Model.Card>("PutCards", Client.ServerIp, Client.ServerPort, Client.PutCard);
            }

            if (task == Model.Task.TaskNature.GET_SCORES)
            {
                //Console.WriteLine("Client will GET THE SCORES");
                NetworkComms.SendObject<int>("GetScores", Client.ServerIp, Client.ServerPort, Client.Player.Id);
            }

            if (task == Model.Task.TaskNature.WAIT)
            {
                //Display status of the game
            }
        }

        /// <summary>   Calls DoAction with the task.Type parameter </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="connection">   The connection. </param>
        /// <param name="task">         The task. </param>

        public void ReceiveAction(PacketHeader header, Connection connection, Model.Task task)
        {
            //Console.WriteLine("Receive action type :"+task.Type);
            Client.Player.TaskState.Type = task.Type;
            DoActions(task.Type);
        }

        /// <summary>   Gets the player's hand </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="connection">   The connection. </param>
        /// <param name="cards">        The cards. </param>

        public void GetHand(PacketHeader header, Connection connection, List<Model.Card> cards)
        {
            //Console.WriteLine("IN GET HAND CLIENT ROUTER\n");
            Client.Player.Hand = cards;
            Services.DisplayHand(Client.Player);
        }

        /// <summary>   Displays the player's score </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="connection">   The connection. </param>
        /// <param name="scoreBoard">   The score board. </param>

        public void GetScores(PacketHeader header, Connection connection, ScoreBoard scoreBoard)
        {
            /*Console.WriteLine("IN GET SCORE CLIENT ROUTER\n");
            Console.WriteLine("IN GET SCORE CLIENT ROUTER TEAM 1 SCORE :"+scoreBoard.ScoreTeams[0]+"\n");
            Console.WriteLine("IN GET SCORE CLIENT ROUTER TEAM 2 SCORE :" + scoreBoard.ScoreTeams[1] + "\n");*/
            //End of the Game
            if (Client.Player.Hand.Count == 0)
            {
                Services.DisplayEndGame(scoreBoard, Client.Player);
            }
            else
            {
                Services.DisplayEndFold(scoreBoard);
            }
        }

        /// <summary>   Displays the game Board </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="connection">   The connection. </param>
        /// <param name="board">        The board. </param>

        public void GetBoard(PacketHeader header, Connection connection, Model.Board board)
        {
            //Console.WriteLine("TRUMP Value IN GET BOARD TYPE:" + board.Trump.Type + " VALUE :" + board.Trump.Val + "\n");
            Client.Board = board;
            Services.DisplayBoard(board);
            Services.DisplayHand(Client.Player);
        }

        /// <summary>   Player putting card on the Fold and removing it from its hand</summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="connection">   The connection. </param>
        /// <param name="result">       True to result. </param>

        public void PutCard(PacketHeader header, Connection connection, bool result)
        {
            if (result == false)
            {
                Console.WriteLine("Error occured in the Server, please try again");
            }
            else
            {
                Client.Player.Hand.Remove(Client.PutCard);
                Console.WriteLine("Waiting for Others players...\n");
            }
        }

        /// <summary>   Displays the trump. </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="connection">   The connection. </param>
        /// <param name="trump">        The trump. </param>

        public void GetTrump(PacketHeader header, Connection connection, Card trump)
        {
            Response response = new Response();
            Client.Board.Trump = trump;
            Services.DisplayTrump(Client.Board);
            Console.WriteLine("Please type Y/N to get the trump:");
            Client.Prompt = true;
            response.Text = Console.ReadLine();
            Client.Prompt = false;
            response.Id = Client.Player.Id;
            NetworkComms.SendObject<Model.Response>("Responses", Client.ServerIp, Client.ServerPort, response);
        }

        /// <summary>   Displays greeting's informations </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="connection">   The connection. </param>
        /// <param name="greeting">     The greeting. </param>
        public void Greeting(PacketHeader header, Connection connection, Greeting greeting)
        {
            //Check if connection is OK
            if (greeting.Connection == false) {
                Environment.Exit(1);
            }

            //Get Data from the HandShake
            Client.Player.Id = greeting.Id;
            Client.Player.Team = greeting.Team;
            Console.WriteLine(greeting.Message + " , your will play with id :"+greeting.Id+" and are in the TEAM "+greeting.Team);
        }
    }
}
