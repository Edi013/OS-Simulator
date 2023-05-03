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
            Console.WriteLine("Input command's name :\n");
            
            string userInput = Console.ReadLine().ToLower();
            if (String.IsNullOrWhiteSpace(userInput))
            {
                throw new CancelCommandException();
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
            Console.WriteLine("If needed, input 'Y' to shut down:");

            if(Console.ReadLine() == "Y")
                return true;
            return false;
        }
        
    }
}
