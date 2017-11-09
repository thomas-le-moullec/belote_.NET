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
        bool trump;

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

        public bool Trump
        {
            get { return trump; }
            set { trump = value; }
        }

        public Card()
        {
            Type = new Types();
            Val = "";
            Points = 0;
            Trump = false;
        }
    }
}
