using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Model
{
    public class Room
    {
        int id;
        int idTurn;
        int trumpTaker;

        public Room()
        {
            IdTurn = 1;
            TrumpTaker = -1;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdTurn
        {
            get { return idTurn; }
            set { idTurn = value; }
        }

        public int TrumpTaker
        {
            get { return trumpTaker; }
            set { trumpTaker = value; }
        }
    }
}
