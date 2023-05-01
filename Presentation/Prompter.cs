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
        public void DisplayCommands(List<ICommand> comands)
        {
            Console.WriteLine("Commands :");
            foreach (var command in comands)
            {
                Console.WriteLine($"{command.Name} - {command.Description}");
            }
            Console.WriteLine();
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

        public bool WantsToExit()
        {
            Console.WriteLine("If needed, input 'Y' to shut down:");

            if(Console.ReadLine() == "Y")
                return true;
            return false;
        }
        public void WelcomeUser()
        {
            Console.WriteLine("Welcome, user!");
            Console.WriteLine("You are using Ghenea Eduard's OS\n\n");
        }
        public void Cursor()
        {
            int milisecondsToWait = 350;

            Console.Write(">");
            while (true)
            {
                Console.Write(">_");
                Thread.Sleep(milisecondsToWait);
                Console.Write("\b \b \b \b");
                Thread.Sleep(milisecondsToWait);
                Console.Write(">");
                Thread.Sleep(milisecondsToWait);
                Console.Write("\b \b");
                /*Console.Write(">");
                Thread.Sleep(milisecondsToWait);*/
            }
            /*
             for main :
            //Thread cursorThread = new Thread(prompter.Cursor);
                //cursorThread.Start();
            */
        }
    }
}
