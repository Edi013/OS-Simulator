using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class DirCommandPresentation : IPresentCommand
    {
        public string Name => "dir";
        public string Description => "Display every file in current directory";
        public string Structure => "dir -argument(s)";
        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
                new CommandArgument()
                {
                    Name = "-a",
                    Description = "Display more information for each file.",
                },
            };

    }
}
