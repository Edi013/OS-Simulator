using System.Reflection.Emit;

namespace PrivateOS.Business
{
    public class DirCommand : ICommand
    {
        public List<string> actualArguments { get; }

        List<string> ICommand.actualArguments => throw new NotImplementedException();

        public DirCommand(List<string> arguments)
        {
            actualArguments = arguments;
        }

        public void Execute(HWStorage hwStorage)
        {
            // Se afiseaza un warning daca exista mai mult de 1 argument specificat

            Console.WriteLine("Files:");

            WarningMaxNoOfArgs(1);

            //In functie de ce argument exista, executam corespunzator comanda
            if (!actualArguments.Any())
            {
                // Fara argumente, executa comanda "dir"
                ExecuteWithNoArguments(hwStorage);
                return;
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

        public void WarningMaxNoOfArgs(int maxNoOfArgs)
        {
            if (actualArguments.Count > maxNoOfArgs)
                Prompter.NoImplementionForMoreThanNoOfArgs(maxNoOfArgs);
        }

        /*
         Metodele pentru afisare sunt similare.
         Difera doar detaliile afisate
        */
        private void ExecuteWithNoArguments(HWStorage  hwStorage)
        {
            bool fileFound = false;
            foreach (RoomTuple entry in hwStorage.ROOM.table)
            {
                if (entry != null && entry.name != "?")
                {
                    fileFound = true;
                    Console.WriteLine(entry.DisplayMinimalDetails() );
                }
            }

            if (!fileFound)
                Console.WriteLine("No files are present.");
        }
        private void ExecuteWithAllArgument(HWStorage  hwStorage)
        {
            int contor = 0;
            foreach (RoomTuple entry in hwStorage.ROOM.table)
            {
                if (entry != null && entry.name != "?")
                {
                    contor++;
                    Console.WriteLine(entry.DisplayAllDetails());
                }
            }

            if (contor == 0)
                Console.WriteLine("No files are present.");
        }
    }
}
