﻿using System.Configuration;

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
                List<ushort> allocationChainFromFatToCopyTo
                    = CommonCommandMethods
                        .AllocateResourcesForNewFile
                        (storage, newName, newExtension, entryToFileToCopy.size);

                //Initializarea noului lant de alocare 
                storage.InitialiazeClusters(allocationChainFromFatToCopyTo);

                //identificarea AU de copiat
                List<ushort> allocationChainFromFatToCopy 
                    = storage.FAT
                    .ReadChain(entryToFileToCopy.firstAllocationUnit);

                //Copierea efectiva
                storage
                    .CopyChainValuesIntoGivenChain
                    (allocationChainFromFatToCopy, allocationChainFromFatToCopyTo);

            }
            catch(Exception e)
            {
                throw;
            }
        }

    }
}
