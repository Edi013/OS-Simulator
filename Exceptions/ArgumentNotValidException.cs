using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class ArgumentNotValidException : Exception
    {
        public ArgumentNotValidException(string message = "Argument not valid.")
            : base(message)
        {
        }
    }
}
