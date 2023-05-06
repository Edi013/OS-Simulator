using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class DeleteCommandPresentation : IPresentCommand
    {
        public string Name => "delete";

        public string Description => "Deletes a file with given name and extension.";
        public string Structure => "delete file_name.extension";
        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
            };
    }
}
