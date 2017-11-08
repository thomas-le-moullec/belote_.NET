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
<<<<<<< HEAD
        private int id;
        private int trumpTaker;
        private List<Player> players;
        private Board roomBoard;
=======
        int id;
        int trumpTaker;
        List<Player> players;
        Board roomBoard;
>>>>>>> c35f5986f8c5502a006abfa66b1923f9fd840c8e

        public Room()
        {
            Id = -1;
            TrumpTaker = -1;
            Players = new List<Player>();
            RoomBoard = new Board();
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int TrumpTaker
        {
            get { return trumpTaker; }
            set { trumpTaker = value; }
        }

        public List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        public Board RoomBoard
        {
            get { return roomBoard; }
            set { roomBoard = value; }
        }

    }
}
