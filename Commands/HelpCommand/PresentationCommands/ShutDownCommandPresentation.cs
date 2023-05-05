using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class ShutDownCommandPresentation : IPresentCommand
    {
        public string Name => "\" \"";
        public string Description => "Initiate shut down.";
        public string Structure => "\" \"";
        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
            };

    }
}
