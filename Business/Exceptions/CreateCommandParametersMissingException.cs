using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public  class CreateCommandParametersMissingException : Exception
    {
        public CreateCommandParametersMissingException(string message = "One of create command parameters is missing.")
            : base(message)
        {
        }
    }
}
