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


        public void Execute(HWStorage storage)
        {
            // Parsam 2 argumente, 4 denumiri
            // Daca nu exista fisierul -> throw (early return)
            // --mecanismul de copiere--
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

                //alocarea resurselor fisierului nou
                List<ushort> allocationChainFromFat
                    = CommonCommandMethods
                        .AllocateResourcesForNewFile
                        (storage, entryToFileToCopy.name, entryToFileToCopy.extension, entryToFileToCopy.size);

                //identificarea AU de copiat
                List<ushort> allocationChainElementToCopyFromFat 
                    = storage.FAT
                    .ReadChain(entryToFileToCopy.firstAllocationUnit);

                //Copierea efectiva
                storage
                    .CopyChainValuesIntoGivenChain
                    (allocationChainFromFat, allocationChainElementToCopyFromFat);

            }
            catch(Exception e)
            {
                throw;
            }
        }

    }
}
