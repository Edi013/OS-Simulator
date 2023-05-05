using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class FileDoesNotExistsException : Exception
    {
        public FileDoesNotExistsException(string message = "File doesn't exists.")
            : base(message)
        {
        }
    }
}
