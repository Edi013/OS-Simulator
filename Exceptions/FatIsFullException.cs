using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class FatIsFullException : Exception
    {
        public FatIsFullException(string message = "Fat is full.")
            : base(message)
        {
        }
    }
}
