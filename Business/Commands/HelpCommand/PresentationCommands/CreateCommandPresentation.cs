using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class CreateCommandPresentation : IPresentCommand
    {
        public string Name => "create";

        public string Description => "Create a file with given name, extension, size and type of content.";
        public string Structure => "create file_name.extension size_in_bytes content_type";
        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
                new CommandArgument()
                {
                    Name = "-alfa",
                    Description = "Generates alfabetic characters until given size is full",
                },
                new CommandArgument()
                {
                    Name = "-num",
                    Description = "Generates numbers, 0-9, until given size is full",
                },
                new CommandArgument()
                {
                    Name = "-hex",
                    Description = "Generates hexazecimal numbers, 0-9, until given size is full",
                },
            };

    }
}
