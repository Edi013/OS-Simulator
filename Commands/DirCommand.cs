namespace PrivateOS.Business
{
    public class DirCommand : ICommand
    {
        public string Name => "dir";
        public string Description => "Display every file in current directory";

        public List<CommandArgument> Arguments => 
            new List<CommandArgument>() 
            {
            };

        public DirCommand()
        {
        }

        public void Execute(OS os)
        {
            Console.WriteLine("Files:");

            int contor = 0;
            foreach(var entry in os.room.table)
            {
                if(entry != null)
                {
                    contor++;
                    Console.WriteLine(entry.ToString());
                }
            }

            if(contor == 0)
                Console.WriteLine("No files are present.");
        }
    }
}
