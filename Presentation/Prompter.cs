using PrivateOS.Business;

namespace PrivateOS
{
    public class Prompter
    {
        public Prompter()
        {
        }

        public string AskForCommand()
        {
            string userCommand = GetCommandFromUser();
            return userCommand;
        }
        private string GetCommandFromUser()
        {
            string userInput = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(userInput))
            {
                throw new ShutDownException();
            }

            return userInput;
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
            Console.WriteLine("Input 'Y' to shut down:");

            if(Console.ReadLine().ToUpper() == "Y")
                return true;
            return false;
        }
        public static void NoImplementionForMoreThanNoOfArgs(int noOfArgs)
        {
            Console.WriteLine($"No implementation exists for more than {noOfArgs} argument!\n");
        }
    }
}
