using System.Reflection.Emit;

namespace PrivateOS.Business
{
    public class HelpCommand : ICommand
    {
        //Primele 3 proprietati sunt pentru Presentation Layer
        public string Name => "dir";
        public string Description => "Display every file in current directory";

        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {

            };

        //Ultima + Metodele - Business layer
        public List<string> actualArguments { get; }

        public HelpCommand(List<string> arguments)
        {
            actualArguments = arguments;
        }


        public void Execute(HWStorage hwStorage)
        {

            
        }
     

    }
}
