using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class RoomIsFullException : Exception
    {
        public RoomIsFullException(string message = "ROOM is full.")
            : base(message)
        {
        }
    }
}
