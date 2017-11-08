using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Task
    {
        /**
         * \enum TASKS
         */
        public enum Tasks
        {
            WAIT,
            PUT_CARD,
            GET_TRUMP,
            GET_CARD,
            GET_BOARD
        }

        Tasks type;

        public Tasks Type
        {
            get { return type; }
            set { type = value; }
        }

        public Task()
        {
            Type = new Tasks();
        }
    }
}
