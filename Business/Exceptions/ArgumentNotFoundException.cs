using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class ArgumentNotFoundException : Exception
    {
        public ArgumentNotFoundException(string message = "Argument not found.")
            : base(message)
        {
        }
    }
}
