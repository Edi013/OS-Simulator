using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class CommandNotFound : Exception
    {
        public CommandNotFound(string message = "Command not found.")
            : base(message)
        {
        }
    }
}
