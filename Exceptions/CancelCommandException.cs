using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class CancelCommandException : Exception
    {
        public CancelCommandException(string message = "Command canceld.")
            : base(message)
        {
        }
    }
}
