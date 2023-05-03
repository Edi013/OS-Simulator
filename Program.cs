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
                        var command = prompter.GetCommandFromUser(commands);
                        
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