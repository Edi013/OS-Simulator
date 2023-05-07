using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class RenameCommandPresentation : IPresentCommand
    {
        public string Name => "rename";

        public string Description => "Replaces the name of a file. If the file doesn't exists, warning will be displayed.";
        public string Structure => "rename file_old_name.extension file_new_name.extension";
        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
            };
    }
}
