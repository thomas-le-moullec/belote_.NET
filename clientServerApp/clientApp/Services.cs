using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ClientApplication;

namespace clientApp
{
    public class Services
    {
        /// <summary>   Puts a card on the fold </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="client">   The client. </param>
        ///
        /// <returns>   A Card. </returns>

        public static Card PutCard(Client client)
        {
            Console.WriteLine("Please Choose which card do you want to play with this format : [TYPE]-[VALUE] (e.g CLUB:Q) :");
            client.Prompt = true;
            bool tryAgain = true;
            Card putCard = new Card();
            while (tryAgain)
            {
                string response = Console.ReadLine();
                try
                {
                    putCard = Card.VerifCard(client.Player, response, client.Board);
                    tryAgain = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("CHEATER : Play a correct card.", e);
                }
            }
            client.Prompt = false;
            return putCard;
        }

        /// <summary>   Displays the board </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="board">    The board. </param>

        public static void DisplayBoard(Board board)
        {
            Console.WriteLine("########################### CURRENT FOLD ###########################\n");
            foreach (var card in board.Fold)
            {
                Console.WriteLine(">Type : "+card.Type+"\t\t\t Value : "+card.Val+" \t\t\t Player("+card.IdPlayer+")\t\t\t Points : "+card.Points+"\n");
            }
            Console.WriteLine("------------------------------------------------------------\n");
            DisplayTrump(board);
        }

        /// <summary>   Displays the player's hand </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="player">   The player. </param>

        public static void DisplayHand(Player player)
        {
            Console.WriteLine("########################### YOUR HAND ###########################\n");
            foreach (var card in player.Hand)
            {
                Console.WriteLine(">Type : " + card.Type + "\t\t\t Value : " + card.Val + "\t\t\t Points : "+card.Points+"\n");
            }
            Console.WriteLine("------------------------------------------------------------\n");
        }

        /// <summary>   Displays the trump </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="board">    The board. </param>

        public static void DisplayTrump(Board board)
        {
            Console.WriteLine("########################### TRUMP ###########################\n");
            Console.WriteLine(">Type : " + board.Trump.Type + "\t\t\t Value : " + board.Trump.Val + "\t\t\t Points : " +board.Trump.Points+ "\n");
            Console.WriteLine("------------------------------------------------------------\n");
        }

        /// <summary>   Displays the game's results </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="scoreBoard">   The score board. </param>
        /// <param name="player">       The player. </param>

        public static void DisplayEndGame(ScoreBoard scoreBoard, Player player)
        {
            Console.WriteLine("########################### END OF THE GAME ###########################\n");
            Console.WriteLine("RESULTS:\n");
            Console.WriteLine("Team 1 Finished with "+scoreBoard.ScoreTeams[0]+" points\n");
            Console.WriteLine("Team 2 Finished with " + scoreBoard.ScoreTeams[1] + " points\n");
            if (player.Team == 0 && scoreBoard.ScoreTeams[0] < scoreBoard.ScoreTeams[1])
            {
                Console.WriteLine("Sorry you loose this one !\n");
            }
            else
            {
                Console.WriteLine("Congratulations, you win this one !\n");
            }
            Console.WriteLine("########################### <3 BYE <3 ###########################\n");
        }

        /// <summary>   Displays the fold's results </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="scoreBoard">   The score board. </param>

        public static void DisplayEndFold(ScoreBoard scoreBoard)
        {
            Console.WriteLine("########################### END OF THE FOLD ###########################\n");
            //Maybe add Username
            Console.WriteLine("Player "+scoreBoard.IdWinnerFold+" won this one with ["+scoreBoard.CardWinnerFold.Type+":"+scoreBoard.CardWinnerFold.Val+"]. He has to play now !\n");
            Console.WriteLine("------------------------------------------------------------\n");
        }
    }
}
