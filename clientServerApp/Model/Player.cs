using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Player
    {
        int id;
        List<Card> hand;
        int score;
        int team;
        Task taskState;
        string username;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public List<Card> Hand
        {
            get { return hand; }
            set { hand = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int Team
        {
            get { return team; }
            set { team = value; }
        }

        public Task TaskState
        {
            get { return taskState; }
            set { taskState = value; }
        }
        public Player()
        {
            Id = -1;
            Hand = new List<Card>();
            Score = 0;
            Team = 0;
            TaskState = new Task();
        }
    }
}
