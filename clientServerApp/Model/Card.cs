using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Model
{
    [ProtoContract]
    public class Card
    {
        public enum Types
        {
            CLUB,
            DIAMOND,
            HEART,
            SPADE
        }

        [ProtoMember(1)]
        Types type;
        [ProtoMember(2)]
        String val;
        [ProtoMember(3)]
        int points;
        [ProtoMember(4)]
        int idPlayer;

        public Types Type
        {
            get { return type; }
            set { type = value; }
        }

        public String Val
        {
            get { return val; }
            set { val = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public int IdPlayer
        {
            get { return idPlayer; }
            set { idPlayer = value; }
        }

        /// <summary>
        /// ** Card Verification **
        /// Convert the input string to a Card object
        /// Checks if the Card is in the player's hand
        /// Checks if the player is allowed to play this card
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="cardStr">User input string</param>
        /// <param name="fold">Fold already on table</param>
        /// <returns></returns>
        public static Card VerifCard(Player player, String cardStr, Board board)
        {
            String[] elems = cardStr.Split(':');
            Card card;

            card = (player.Hand.First(item => (item.Val.Equals(elems[1]) && item.Type.ToString().Equals(elems[0]))));
            if (card == null)
            {
                throw new Exception();
            }
            if (board.Fold.Count() != 0)
            {
                if (card.Type.Equals(board.Fold[0].Type) == false)
                {
                    try
                    {
                        player.Hand.First(item => item.Type.Equals(board.Fold[0].Type));
                    }
                    catch
                    {
                        return (card);
                    }
                    throw new Exception();
                }
                if (card.Type.Equals(board.Fold[0].Type) == false)
                {
                    try
                    {
                        player.Hand.First(item => item.Type.Equals(board.Fold[0].Type));
                    }
                    catch
                    {
                        throw new Exception();
                    }
                    try
                    {
                        player.Hand.First(item => item.Type.Equals(board.Trump.Type));
                    }
                    catch
                    {
                        return (card);
                    }
                    throw new Exception();
                }
            }
            return (card);
        }

        public Card()
        {
            Type = new Types();
            Val = "";
            Points = 0;
        }
    }
}
