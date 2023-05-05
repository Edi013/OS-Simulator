namespace PrivateOS.Business
{
    public class CreateCommand : ICommand
    {
        public string Name => "create";

        public string Description => "Create a file with given name, extension, size and type of content.";

        public List<CommandArgument> Arguments =>
            new List<CommandArgument>()
            {
                new CommandArgument()
                {
                    Name = "-alfa",
                    Description = "Generates alfabetic characters until given size is full",
                },
                new CommandArgument()
                {
                    Name = "-num",
                    Description = "Generates numbers, 0-9, until given size is full",
                },
                new CommandArgument()
                {
                    Name = "-hex",
                    Description = "Generates hexazecimal numbers, 0-9, until given size is full",
                },
            };

        public List<string> actualArguments { get; }

        public CreateCommand(List<string> actualArguments)
        {
            this.actualArguments = actualArguments;
        }

        //create nume.txt 5 -NUM

        public void Execute(HWStorage storage)
        {
            try
            {
                List<string> fileNameAndExtension = actualArguments[0].Split(".").ToList();
                string fileName = fileNameAndExtension[0];
                string fileExtension = fileNameAndExtension[1];
                ushort sizeInBytes = ushort.Parse(actualArguments[1]);
                string contentType = actualArguments[2];

                ValidateArguments(fileName, fileExtension, sizeInBytes, contentType);
                if (!storage.ROOM.ExistsFreeEntry())
                    throw new RoomIsFullException();
                if (!storage.FAT.CheckFreeAllocationChain(sizeInBytes))
                    throw new FatIsFullException();

                //alocate the allocation chain for a file in fat
                List<ushort> allocationChainFromFat = 
                    storage.FAT.AllocateChainForFile(sizeInBytes);

                //get the first free entry in room 
                ushort indexOfFreeEntryInRoom = 
                    storage.ROOM.GetFirstFreeEntry();
                //build and add the tuple in room
                RoomTuple newRoomTuple =  
                    new RoomTuple(fileName, fileExtension, sizeInBytes, allocationChainFromFat[0]);
                storage.ROOM.AddTupleInRoom(indexOfFreeEntryInRoom, newRoomTuple);

                //foreach cluster - a method that finds next cluster - FAT method
                // no needed anymore. we have the whole chain from allocation
                foreach (var indexFromFat in allocationChainFromFat)
                {
                    var allocationUnitFromStorage = storage.GetStorageCluster(indexFromFat);
                    WriteFile(AllocationUnit cluster, string contentType);

                }
                //write whatever the argument is. -  switch for args
                // until the size is reached. - flag / state var
                // you need to remember what was the last letter written in last cluster - enumerator
            }
            catch(Exception e)
            {
                throw;
            }

            //verificam daca esti free entry in room/fat
            //alocam spatiu pe fat
            //alocam spatiu in room

            //scriem pe disk - switch pt argumente:
            // - trebuie
 
        }

        private void WriteFile()
        private void ValidateArguments(string fileName, string fileExtension, ushort sizeInBytes, string contentType)
        {
            if (fileName == null || fileExtension == null || sizeInBytes == null || contentType == null)
                throw new ArgumentNotFoundException();
            if (fileName.Length == 0 || fileExtension.Length == 0  || sizeInBytes == 0 || contentType.Length == 0)
                throw new ArgumentNullException("Missing mandatory argument - create command");

            bool validContentType = false;
            foreach (var argument in actualArguments)
            {
                if(argument.Equals(contentType))
                    validContentType = true;
            }
            if (!validContentType)
                throw new ArgumentNotValidException();
        }
    }
}
