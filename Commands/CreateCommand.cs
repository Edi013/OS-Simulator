using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class CreateCommand : ICommand
    {
        public string Name => "create";

        public string Description => "Create a file with given name, extension, size and type of content.";

        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
            };

        public void Execute(OS os)
        {
            throw new NotImplementedException();
        }
    }
}
