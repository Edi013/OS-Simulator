using System.Reflection.Emit;

namespace PrivateOS.Business
{
    public class DirCommand : ICommand
    {
        //Primele 3 proprietati sunt pentru Presentation Layer
        public string Name => "dir";
        public string Description => "Display every file in current directory";

        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
                new CommandArgument()
                {
                    Name = "-a",
                    Description = "Display more information for each file",
                },
            };

        //Ultima + Metodele - Business layer
        public List<string> actualArguments { get; }

        public DirCommand(List<string> arguments)
        {
            actualArguments = arguments;
        }


        public void Execute(HWStorage hwStorage)
        {

            Console.WriteLine("Files:");

            // comanda "dir"
            if (!actualArguments.Any())
            {
                ExecuteWithNoArguments(hwStorage);
                return;
            }
            if(actualArguments.Count > 1)
            {
                Console.WriteLine("No implementation exists for more than 1 argument!\n");
            }
            switch (actualArguments.First().ToLower())
            {
                // comanda "dir "
                case "":
                    ExecuteWithNoArguments(hwStorage);
                    break;
                // comanda "dir -a"
                case "-a":
                    ExecuteWithAllArgument(hwStorage);
                    break;

                default:
                    throw new ArgumentNotFoundException();
            }
        }
        private void ExecuteWithNoArguments(HWStorage  hwStorage)
        {
            int contor = 0;
            foreach (RoomTuple entry in hwStorage.ROOM.table)
            {
                if (entry != null)
                {
                    contor++;
                    Console.WriteLine(entry.DisplayMinimalDetails() );
                }
            }

            if (contor == 0)
                Console.WriteLine("No files are present.");
        }
        private void ExecuteWithAllArgument(HWStorage  hwStorage)
        {
            int contor = 0;
            foreach (RoomTuple entry in hwStorage.ROOM.table)
            {
                if (entry != null)
                {
                    contor++;
                    Console.WriteLine(entry.DisplayAllDetails() );
                }
            }

            if (contor == 0)
                Console.WriteLine("No files are present.");
        }

    }
}
