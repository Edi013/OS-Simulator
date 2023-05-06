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
                new DeleteCommandPresentation(),
                new RenameCommandPresentation(),
            };
        }

        public void Execute(HWStorage hwStorage)
        {
            // Executa presentarea fiecarei comenzi.
            // Se afiseaza un warning daca exista argumente specificate intrucat comanda nu are argumente valide
            WarningMaxNoOfArgs(0);

            Console.WriteLine("Commands:\n");
            foreach (var presentationCommand in presentationCommands)
            {
                Console.WriteLine($"\t{presentationCommand.Name} : {presentationCommand.Description}");
                Console.WriteLine($"\tStructure: {presentationCommand.Structure}");
                Console.WriteLine("\tArguments:");
                if(!presentationCommand.Arguments.Any())
                {
                    Console.WriteLine("No arguments.");
                    Console.WriteLine();
                    continue;
                }
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
