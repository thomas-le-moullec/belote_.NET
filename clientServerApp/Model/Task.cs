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
        public enum TaskNature
        {
            WAIT,
            GREETINGS,
            ASKFORTASK,
            PUT_CARD,
            GET_TRUMP,
            GET_CARD,
            GET_BOARD,
            GET_HAND
        }

        TaskNature type;

        public TaskNature Type
        {
            get { return type; }
            set { type = value; }
        }

        public Task()
        {
            Type = new TaskNature();
        }
    }
}
