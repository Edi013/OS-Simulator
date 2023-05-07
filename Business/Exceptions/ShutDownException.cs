using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class ShutDownException : Exception
    {
        public ShutDownException(string message = "Shut Down Command.")
            : base(message)
        {
        }
    }
}
