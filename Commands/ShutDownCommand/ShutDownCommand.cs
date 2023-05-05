using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class ShutDownCommand : ICommand
    {
        public string Name => "\" \"";

        public string Description => "White space - cancels command input to allow the user to shut down his machine";

        public List<string> actualArguments { get; }

        public ShutDownCommand(List<string> actualArguments)
        {
            this.actualArguments = actualArguments;
        }

        public void Execute(HWStorage hwStorage)
        {
            WarningMaxNoOfArgs(0);
            return;
        }
        public void WarningMaxNoOfArgs(int maxNoOfArgs)
        {
            Prompter.NoImplementionForMoreThanNoOfArgs(maxNoOfArgs);
        }
    }
}
