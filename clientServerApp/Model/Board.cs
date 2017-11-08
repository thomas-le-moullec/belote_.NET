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

        public Board()
        {
            Fold = new List<Card>();
            idTurn = -1;
            trump = new Card();
        }
    }
}
