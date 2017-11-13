using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Model
{
    [ProtoContract]
    public class ScoreBoard
    {
        [ProtoMember(1)]
        int idWinnerFold;
        [ProtoMember(2)]
        Card cardWinnerFold;
        [ProtoMember(3)]
        int scoreTeam0;
        [ProtoMember(4)]
        int scoreTeam1;

        public ScoreBoard()
        {
            IdWinnerFold = 0;
            ScoreTeam0 = 0;
            ScoreTeam1 = 0;
        }
        public int IdWinnerFold
        {
            get { return idWinnerFold; }
            set { idWinnerFold = value; }
        }

        public Card CardWinnerFold
        {
            get { return cardWinnerFold; }
            set { cardWinnerFold = value; }
        }
        public int ScoreTeam0
        {
            get { return scoreTeam0; }
            set { scoreTeam0 = value; }
        }

        public int ScoreTeam1
        {
            get { return scoreTeam1; }
            set { scoreTeam1 = value; }
        }
    }
}
