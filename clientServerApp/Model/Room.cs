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
        private int id;
        private int trumpTaker;
        private List<Player> players;
        private Board roomBoard;
        private ScoreBoard scoreBoard;

        public Room()
        {
            Id = -1;
            TrumpTaker = -1;
            Players = new List<Player>();
            RoomBoard = new Board();
            ScoreBoard = new ScoreBoard();
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public ScoreBoard ScoreBoard
        {
            get { return scoreBoard; }
            set { scoreBoard = value; }
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
