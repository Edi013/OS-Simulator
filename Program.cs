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
                };

                prompter.WelcomeUser();
                
                while (true)
                {
                    var command = prompter.GetCommandFromUser(commands);
                    
                    command.Execute(os);
                   
                    if (prompter.WantsToExit())
                    {
                        break;
                    }
                }
            }
            catch (CancelCommandException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happend, message: \n" + e.Message +"\n" + e.StackTrace);
            }
        }
    }
}