using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string message = "Command not found.")
            : base(message)
        {
        }
    }
}
