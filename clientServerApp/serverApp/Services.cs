using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace serverApp
{
    public class Services
    {
        public Services()
        {
        }

        /// <summary>
        /// ** Card Distribution **
        /// Add random cards from pick to player's hand
        /// Remove the card that has just been added to the player's hand from the pick
        /// </summary>
        /// <param name="player">Player we are distributing the cards to</param>
        /// <param name="pick">Card list from which we take the cards</param>
        /// <param name="number">Number of cards we distribute</param>
        public static void DistribCards(Player player, List<Card> pick, int number)
        {
            Random rnd = new Random();
            int range;

            for (int i = 0; i < number; i++)
            {
                range = rnd.Next(0, pick.Count());
                pick[range].IdPlayer = player.Id;
                player.Hand.Add(pick[range]);
                pick.RemoveAt(range);
            }
        }

        /// <summary>
        /// ** Generates a trump **
        /// Chose trump from a random card taken in the pick and remove this card from the pick
        /// </summary>
        /// <param name="board">Contains the trump</param>
        public static void TrumpGeneration(Board board)
        {
            Random rnd = new Random();
            int range;

            range = rnd.Next(0, board.Pick.Count());
            board.Trump = board.Pick[range];
            board.Pick.RemoveAt(range);
        }

        /// <summary>   Distributes the trump to the player who wants it </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="player">   Player we are distributing the cards to. </param>
        /// <param name="board">    Contains the trump. </param>
        public static void DistribTrump(Player player, Board board)
        {
            player.Hand.Add(board.Trump);
            board.Trump.IdPlayer = player.Id;
        }

        /// <summary>
        /// ** Playing a card **
        /// Put players's chosen card in the board's fold
        /// Take player's chosen card off player's hand
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="board">Board</param>
        /// <param name="cardStr">Player's input string</param>
        public static void PutCard(Player player, Board board, Card card)
        {
            board.Fold.Add(card);
            player.Hand.Remove(card);
        }

        /// <summary>
        /// ** Reset the cards' points given to the trump type
        /// </summary>
        /// <param name="room"></param>
        public static void SetCardPoints(Room room)
        {
            // points des cartes des joueurs
            foreach (Player player in room.Players)
            {
                foreach (Card card in player.Hand)
                {
                    if (card.Type.Equals(room.RoomBoard.Trump.Type) && card.Val.Equals("J"))
                        card.Points = 20;
                    if (card.Type.Equals(room.RoomBoard.Trump.Type) && card.Val.Equals("9"))
                        card.Points = 14;
                }
            }
        }

        /// <summary>   Determines fold winner </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="fold">         The fold. </param>
        /// <param name="trumpType">    Type of the trump. </param>
        ///
        /// <returns>   An int. </returns>

        public static int WinFold(Board board)
        {
            bool trumpMax = false;
            int idWinner = -1;
            int bestScore = -1;

        /*    Console.WriteLine("TRUMP TYPE ========== " + board.Trump.Type);
            Console.WriteLine("FOLD TYPE  ========== " + board.Fold[0].Type);*/
            if (board.Fold[0].Type.Equals(board.Trump.Type))
                trumpMax = true;
            /*Console.WriteLine("TRUMP MAX  ========== " + trumpMax);
            Console.WriteLine("ekqjzthkwdurthweilutrherjthekrtheurtyleuityerilutyweilty");*/
            foreach (var card in board.Fold)
            {
                    if (trumpMax == true && card.Type.Equals(board.Trump.Type) && card.Points > bestScore)
                    {
      //                  Console.WriteLine("1ER IF");
                        idWinner = card.IdPlayer;
                        bestScore = card.Points;
                    }
                    else if (trumpMax == false && card.Type.Equals(board.Trump.Type))
                    {
    //                    Console.WriteLine("2EME IF");
                        idWinner = card.IdPlayer;
                        trumpMax = true;
                        bestScore = card.Points;
                    }
                    else if (trumpMax == false && card.Type.Equals(board.Trump.Type) == false && card.Points > bestScore)
                    {
  //                      Console.WriteLine("3EME IF");
                        idWinner = card.IdPlayer;
                        bestScore = card.Points;
                    }
            }
            return (idWinner);
        }

        /// <summary>   Sets the scores. </summary>
        ///
        /// <remarks>   , 13/11/2017. </remarks>
        ///
        /// <param name="fold">     The fold. </param>
        /// <param name="players">  The players. </param>

        public static void SetScores(List<Card> fold, List<Player> players, int nextPlayer)
        {
            //Console.WriteLine("NExt player:"+nextPlayer+"\n");
            foreach (var card in fold)
            {
                players[nextPlayer - 1].Score = players[nextPlayer - 1].Score + card.Points;
            }
        }
    }
}
