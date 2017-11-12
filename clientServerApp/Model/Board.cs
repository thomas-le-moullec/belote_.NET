using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Model
{
    [ProtoContract]
    public class Board
    {
        [ProtoMember(1)]
        List<Card> fold;
        [ProtoMember(2)]
        int idTurn;
        [ProtoMember(3)]
        Card trump;
        [ProtoMember(4)]
        List<Card> pick;

        public List<Card> Fold
        {
            get { return fold; }
            set { fold = value; }
        }

        public int IdTurn
        {
            get { return idTurn; }
            set { idTurn = value; }
        }

        public Card Trump
        {
            get { return trump; }
            set { trump = value; }
        }

        public List<Card> Pick
        {
            get { return pick; }
            set { pick = value; }
        }

        public Board()
        {
            /*List<Card> pick = new List<Card>();
            pick.AddRange(new List<Card>
                    {
                        new Card() { Type = Card.Types.CLUB, Val = "7", Points = 0 },

                     });*/
            Fold = new List<Card>();
            idTurn = -1;
            trump = new Card();
            pick = new List<Card>();
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "7", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "8", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "9", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "10", Points = 10, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "J", Points = 2, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "Q", Points = 3, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "K", Points = 4, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "As", Points = 11, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "7", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "8", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "9", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "10", Points = 10, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "J", Points = 2, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "Q", Points = 3, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "K", Points = 4, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.DIAMOND, Val = "As", Points = 11, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "7", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "8", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "9", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "10", Points = 10, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "J", Points = 2, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "Q", Points = 3, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "K", Points = 4, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "As", Points = 11, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "7", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "8", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "9", Points = 0, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "10", Points = 10, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "J", Points = 2, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "Q", Points = 3, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "K", Points = 4, IdPlayer = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "As", Points = 11, IdPlayer = 0 });
        }
    }
}
