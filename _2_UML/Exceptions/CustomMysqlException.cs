using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_UML.Exceptions
{
    public class CustomMysqlException : Exception
    {
        public CustomMysqlException()
        {
        }

        public CustomMysqlException(string message) : base(message)
        {
        }

        public CustomMysqlException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
