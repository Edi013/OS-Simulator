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
                new CommandArgument()
                {
                    Name = "-ALFA",
                    Description = "Generates alfabetic characters until given size is full",
                },
                new CommandArgument()
                {
                    Name = "-NUM",
                    Description = "Generates numbers, 0-9, until given size is full",
                },
                new CommandArgument()
                {
                    Name = "-HEX",
                    Description = "Generates hexazecimal numbers, 0-9, until given size is full",
                },
            };

        public void Execute(OS os)
        {
            throw new NotImplementedException();
        }
    }
}
