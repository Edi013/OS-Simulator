using System.Configuration;

namespace PrivateOS.Business
{
    public class CopyCommand : ICommand
    {
        public List<string> actualArguments { get; }
        private Iterator charGenerator { get; set; }
        private string[] contentTypes;

        public CopyCommand(List<string> actualArguments)
        {
            this.actualArguments = actualArguments;
            contentTypes = new string[3]
            {
                "-num",
                "-alfa",
                "-hex"
            };
        }
        // parsam 2 argumente, 4 denumiri
        // daca nu exista fisierul -> throw
        // Apleam create command pe : newName.newExtension size -contentType
        public void Execute(HWStorage storage)
        {
            try
            {
                CommonCommandMethods.WarningMaxNoOfArgs(2, actualArguments.Count);

                string oldName;
                string oldExtension;
                string newName;
                string newExtension;
                CommonCommandMethods.ParseArguments(
                    out oldName, out oldExtension, out newName, out newExtension, actualArguments);

                RoomTuple entryToFileToCopy 
                    = CommonCommandMethods.CheckIfFileExists(oldName, oldExtension, storage);

                List<ushort> allocationChainFromFat
                    = CommonCommandMethods
                        .AllocateResourcesForNewFile
                        (storage, entryToFileToCopy.name, entryToFileToCopy.extension, entryToFileToCopy.size);

                //var allocationChainElementToCopyFromFat =  ReadClusters(FAU);
                //WriteClusters(allocationChainFromFat)

            }catch(Exception e)
            {
                throw;
            }
        }

    }
}
