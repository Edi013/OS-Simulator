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
                var commands = new List<ICommand>()
                {
                    new DirCommand(),
                };

                prompter.WelcomeUser();
                
                while (true)
                {
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
    }
}