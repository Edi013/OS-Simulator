using PrivateOS.Business;

namespace PrivateOS
{
    class Bootstraper
    {
        public static void Main()
        {
            try
            {
                var prompter = new Prompter();
                var os = new OS();
                List<ICommand> commands = BuildCommands();

                prompter.WelcomeUser();
                
                while (true)
                {
                    prompter.BeginNewLine();
                    try
                    {
                        string command = prompter.AskForCommand();
                        var commandName = command.Split(" ")[0];
                        var attributes = command.Split(" ").Skip(1);
                        var commandType = CommandResolver.ResolveCommand(commandName, attributes);

                        ICommand comand = commandType.DeclaringType
                        command.Execute(os);
                    }
                    catch (CancelCommandException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (CommandNotFound e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    if (prompter.WantsToExit())
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happend, message: \n" + e.Message +"\n" + e.StackTrace);
            }
        }

        private static List<ICommand> BuildCommands()
        {
            return new List<ICommand>()
                {
                    new DirCommand(),
                    new CreateCommand(),
                };
        }
    }
}