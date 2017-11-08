using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Model
{
    [ProtoContract]
    public class Greeting
    {
        [ProtoMember(1)]
        bool connection;
        [ProtoMember(2)]
        int id;
        [ProtoMember(3)]
        string message;

        public bool Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
