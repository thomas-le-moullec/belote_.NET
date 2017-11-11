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
        Random rnd;
        public Services()
        {
            rnd = new Random();
        }

        /// <summary>
        /// ** Card Distribution **
        /// Add random cards from pick to player's hand
        /// Remove the card that has just been added to the player's hand from the pick
        /// </summary>
        /// <param name="player">Player we are distributing the cards to</param>
        /// <param name="pick">Card list from which we take the cards</param>
        /// <param name="number">Number of cards we distribute</param>
        public void DistribCards(Player player, List<Card> pick, int number) // a voir si l'on ne prend pas un board en param
        {
            int range;

            for (int i = 0; i < number; i++)
            {
                range = rnd.Next(0, pick.Count());
                player.Hand.Add(pick[range]);
                pick.RemoveAt(range);
            }
        }

        /// <summary>
        /// ** Generates a trump **
        /// Chose trump from a random card taken in the pick and remove this card from the pick
        /// </summary>
        /// <param name="board">Contains the trump</param>
        public void TrumpGeneration(Board board)
        {
            int range;

            range = rnd.Next(0, board.Pick.Count());
            board.Trump = board.Pick[range];
            board.Pick.RemoveAt(range);
        }

        /// <summary>
        /// ** Card Verification **
        /// Convert the input string to a Card object
        /// Checks if the Card is in the player's hand
        /// Checks if the player is allowed to play this card
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="cardStr">User input string</param>
        /// <returns></returns>
        public Card VerifCard(Player player, String cardStr)
        {
            String[] elems = cardStr.Split(':');
            Card card;

            card = (player.Hand.First(item => (item.Val == elems[0] && item.Type.ToString() == elems[1])));
            if (card == null)
            {
                throw new Exception();
            }
            return (card);
        }

        /// <summary>
        /// ** Playing a card **
        /// Put players's chosen card in the board's fold
        /// Take player's chosen card off player's hand
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="board">Board</param>
        /// <param name="cardStr">Player's input string</param>
        public void PutCard(Player player, Board board, String cardStr)
        {
            Card card = new Card();
            bool tryAgain = true;
            while (tryAgain)
            {
                try
                {
                    card = VerifCard(player, cardStr);
                    tryAgain = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("CHEATER : {0}", e);
                }
            }
            board.Fold.Add(player.Hand.First(item => item.Equals(card) == true));
            player.Hand.Remove(card);
        }

        /// <summary>
        /// ** Reset the cards' points given to the trump type
        /// </summary>
        /// <param name="room"></param>
        public void SetCardPoints(Room room)
        {
            // points des cartes des joueurs
            foreach (Player player in room.Players)
            {
                foreach (Card card in player.Hand)
                {
                    if (card.Type.Equals(room.RoomBoard.Trump.Type) && card.Val == "V")
                        card.Points = 20;
                    if (card.Type.Equals(room.RoomBoard.Trump.Type) && card.Val == "9")
                        card.Points = 14;
                }
            }
        }

        public Random Rnd
        {
            get { return rnd; }
            set { rnd = value; }
        }
    }
}
