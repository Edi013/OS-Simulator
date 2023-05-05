using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class FileNameTakenException : Exception
    {
        public FileNameTakenException(string message = "File name is already taken.")
            : base(message)
        {
        }
    }
}
