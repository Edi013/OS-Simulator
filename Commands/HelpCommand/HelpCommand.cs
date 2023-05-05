using System.Reflection.Emit;

namespace PrivateOS.Business
{
    public class HelpCommand : ICommand
    {
        private List<IPresentCommand> presentationCommands;
        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
            };
        public List<string> actualArguments { get; }

        public HelpCommand(List<string> arguments)
        {
            actualArguments = arguments;
            presentationCommands = new List<IPresentCommand>() 
            {
                new CreateCommandPresentation(),
                new DirCommandPresentation(),
                new ShutDownCommandPresentation(),
            };
        }

        public void Execute(HWStorage hwStorage)
        {
            WarningMaxNoOfArgs(0);

            Console.WriteLine("Commands:\n");
            foreach (var presentationCommand in presentationCommands)
            {
                Console.WriteLine($"\t{presentationCommand.Name} : {presentationCommand.Description}");
                Console.WriteLine($"\tStructure: {presentationCommand.Structure}");
                Console.WriteLine("\tArguments:");
                foreach (var argument in presentationCommand.Arguments)
                {
                    Console.WriteLine($"\t\t{argument.Name} - {argument.Description}");
                }
                Console.WriteLine();
            }
            
        }
        public void WarningMaxNoOfArgs(int maxNoOfArgs)
        {
            if (actualArguments.Count > maxNoOfArgs)
                Prompter.NoImplementionForMoreThanNoOfArgs(maxNoOfArgs);
        }

    }
}
