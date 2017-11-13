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
        List<int> scoreTeams;

        public ScoreBoard()
        {
            IdWinnerFold = 0;
            ScoreTeams = new List<int>();
            ScoreTeams.Add(0);
            ScoreTeams.Add(0);
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
        public List<int> ScoreTeams
        {
            get { return scoreTeams; }
            set { scoreTeams = value; }
        }
    }
}
