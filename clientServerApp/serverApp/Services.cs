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
                    if (card.Type.Equals(room.RoomBoard.Trump.Type) && card.Val.Equals("V"))
                        card.Points = 20;
                    if (card.Type.Equals(room.RoomBoard.Trump.Type) && card.Val.Equals("9"))
                        card.Points = 14;
                }
            }
        }

        public static int WinFold(List<Card> fold)
        {
            int idWinner = -1;
            int bestScore = -1;

            foreach (var card in fold)
            {
                if (card.Points > bestScore)
                {
                    idWinner = card.IdPlayer;
                }
            }
            return (idWinner);
        }

        public static void SetScores(List<Card> fold, List<Player> players)
        {
            foreach (var card in fold)
            {
                players[card.IdPlayer - 1].Score += card.Points;
            }
        }
    }
}
