using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class NameIsTooLongException : Exception
    {
        public NameIsTooLongException(string message = "Name is too long exception.")
            : base(message)
        {
        }
    }
}
