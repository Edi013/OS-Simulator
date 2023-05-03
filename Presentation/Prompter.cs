using PrivateOS.Business;

namespace PrivateOS
{
    public class Prompter
    {
        public Prompter()
        {
        }

        public ICommand GetCommandFromUser(List<ICommand> comands)
        {
            DisplayCommands(comands);
            return AskForCommand(comands);
        }
        private ICommand AskForCommand(List<ICommand> comands)
        {
            Console.WriteLine("Input command's name (white space to cancel ):\n");
            
            string userInput = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(userInput))
            {
                throw new CancelCommandException();
            }

            foreach(ICommand command in comands)
            {
                if (command.Name == userInput)
                    return command;
            }

            throw new CommandNotFound();
        }
        public void DisplayCommands(List<ICommand> commands)
        {
            Console.WriteLine("Commands :");
            foreach (var command in commands)
            {
                Console.WriteLine($"{command.Name} - {command.Description}");
                Console.WriteLine("Arguments :");
                command.Arguments.ForEach
                //List<CommandArgument> arguments;

                if (!command.Arguments.Any())
                {
                    Console.WriteLine("This method supports no arguments");
                    continue;
                }
                foreach (var argument in command.Arguments)
                {
                    Console.WriteLine("-   " + argument.Name + argument.Description);
                }
            }
            Console.WriteLine();
        }

        public void BeginNewLine()
        {
            Console.Write("Edi_OS>");
        }
        public void WelcomeUser()
        {
            Console.WriteLine("Welcome, user!");
            Console.WriteLine("You are using Ghenea Eduard's OS\n\n");
        }
        public bool WantsToExit()
        {
            Console.WriteLine("If needed, input 'Y' to shut down:");

            if(Console.ReadLine() == "Y")
                return true;
            return false;
        }
        
    }
}
