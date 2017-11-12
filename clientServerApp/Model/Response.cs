using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Model
{using ProtoBuf;
    [ProtoContract]
        public class Response
        {

        [ProtoMember(1)]
        string text;
        [ProtoMember(2)]
        int id;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
