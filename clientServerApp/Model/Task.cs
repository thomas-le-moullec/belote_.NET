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
<<<<<<< HEAD
        public enum TaskNature
        {
            WAIT,
            GREETINGS,
            ASKFORTASK,
=======
        public enum Tasks
        {
            WAIT,
>>>>>>> c35f5986f8c5502a006abfa66b1923f9fd840c8e
            PUT_CARD,
            GET_TRUMP,
            GET_CARD,
            GET_BOARD
        }

<<<<<<< HEAD
        TaskNature type;

        public TaskNature Type
=======
        Tasks type;

        public Tasks Type
>>>>>>> c35f5986f8c5502a006abfa66b1923f9fd840c8e
        {
            get { return type; }
            set { type = value; }
        }

        public Task()
        {
<<<<<<< HEAD
            Type = new TaskNature();
=======
            Type = new Tasks();
>>>>>>> c35f5986f8c5502a006abfa66b1923f9fd840c8e
        }
    }
}
