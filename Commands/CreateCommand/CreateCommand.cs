using System.Configuration;

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
        private Iterator charGenerator { get; set; }

        public CreateCommand(List<string> actualArguments)
        {
            this.actualArguments = actualArguments;
        }

        public void Execute(HWStorage storage)
        {
            try
            {
                string fileName, fileExtension, contentType;
                ushort sizeInBytes;

                ParseArguments(storage, out fileName, out fileExtension, out sizeInBytes, out contentType);

                if (!storage.ROOM.ExistsFreeEntry())
                    throw new RoomIsFullException();
                if (!storage.FAT.CheckFreeAllocationChain(sizeInBytes))
                    throw new FatIsFullException();

                //alocate the allocation chain for a file in fat
                //and save the cluseters to write the file 
                List<ushort> allocationChainFromFat = 
                    storage.FAT.AllocateChainForFile(sizeInBytes);

                //get the first free entry in room 
                ushort indexOfFreeEntryInRoom =
                    (ushort)storage.ROOM.GetFirstFreeEntry();
                //build and add the tuple in room
                RoomTuple newRoomTuple =  
                    new RoomTuple(fileName, fileExtension, sizeInBytes, allocationChainFromFat[0]);
                storage.ROOM.AddTupleInRoom(indexOfFreeEntryInRoom, newRoomTuple);

                //calculate how many chars will be written in given size
                int charSize = int.Parse(ConfigurationManager.AppSettings["CharSizeInBytes"]);
                int fileSizeByCharSize = sizeInBytes / charSize;

                int clusterCharCapacity = 
                    int.Parse(ConfigurationManager.AppSettings["AllocationUnitSize"]) / charSize;

                //start writing
                charGenerator = new Iterator(contentType);
                WriteFile(storage, allocationChainFromFat, fileSizeByCharSize, clusterCharCapacity);
            }catch(Exception e)
            {
                throw;
            }
            
        }

        private void ParseArguments(HWStorage storage, out string fileName, out string fileExtension, out ushort sizeInBytes, out string contentType)
        {
            //Parsing the arguments accordingly to restrictions from room's table entries 
            try
            {

                if (actualArguments.Count < 3)
                    throw new CreateCommandParametersMissingException();

                List<string> fileNameAndExtension = actualArguments[0].Split(".").ToList();
                if(fileNameAndExtension.Count < 2)
                    throw new CreateCommandParametersMissingException();


                fileName = fileNameAndExtension[0];
                int maximumAllowedNoOfCharsAsNameInRoom = (int)Math.Floor
                    (double.Parse(ConfigurationManager.AppSettings["RoomEntrySizeOfName"]) / double.Parse(ConfigurationManager.AppSettings["CharSizeInBytes"]));
                
                if (fileName.Length > maximumAllowedNoOfCharsAsNameInRoom)
                    throw new NameIsTooLongException($"File name is too long.\nMaximum number of chars allowed is {maximumAllowedNoOfCharsAsNameInRoom}");
                CheckForTakenFileName(storage, fileName);

                fileExtension = fileNameAndExtension[1];
                int maximumAllowedNoOfCharsAsExtensionInRoom = (int)Math.Floor
                    (double.Parse(ConfigurationManager.AppSettings["RoomEntrySizeOfName"]) / double.Parse(ConfigurationManager.AppSettings["CharSizeInBytes"]));
                if (fileName.Length > maximumAllowedNoOfCharsAsExtensionInRoom)
                    throw new NameIsTooLongException($"File extension is too long.\nMaximum number of chars allowed is {maximumAllowedNoOfCharsAsNameInRoom}");

                sizeInBytes = ushort.Parse(actualArguments[1]);
                contentType = actualArguments[2];

                ValidateArguments(fileName, fileExtension, sizeInBytes, contentType);

                fileExtension = fileExtension.ToLower();
                contentType = contentType.ToLower();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CheckForTakenFileName(HWStorage storage, string fileName)
        {
            foreach (var entry in storage.ROOM.table)
            {
                if(entry != null && fileName.Equals(entry.name))
                    throw new FileNameTakenException();
            }
        }
        private void ValidateArguments(string fileName, string fileExtension, ushort sizeInBytes, string contentType)
        {
            if (fileName == null || fileExtension == null || sizeInBytes == null || contentType == null)
                throw new ArgumentNotFoundException();

            if (fileName.Length == 0 || fileExtension.Length == 0  || sizeInBytes == 0 || contentType.Length == 0)
                throw new ArgumentNullException("Missing mandatory argument - create command");

            //if (!contentType.Equals("-num") && !contentType.Equals("-alfa") && !contentType.Equals("-hex"))
            foreach (var item in Arguments)
            {
                if (item.Name.Equals(contentType))
                    return;
            }   
            throw new ArgumentNotValidException("Content type asked in create command is not valid!");
        }

        private void WriteFile(HWStorage storage, List<ushort> allocationChainFromFat, int fileSizeByCharSize, int clusterCharCapacity)
        {
            //Incepem scriere pe disc.
            //Pentru fiecare cluster - daca nu e ultimul, scriem capacitatea maxima admisa.
            //                       - daca  e ultimul, scriem diferenta mai jos calculata.
            for (int clusterNo = 0; clusterNo < allocationChainFromFat.Count; clusterNo++)
            {
                var allocationUnitFromStorage = 
                    storage.GetStorageCluster(
                        allocationChainFromFat[clusterNo]);

                if(clusterNo == allocationChainFromFat.Count - 1)
                {
                    int remainingNoOfChars = fileSizeByCharSize - (clusterNo * clusterCharCapacity);
                    WriteCluster(allocationUnitFromStorage, remainingNoOfChars);
                    break;
                }
                WriteCluster(allocationUnitFromStorage, clusterCharCapacity);
            }
        }
        private void WriteCluster(AllocationUnit cluster, int noOfCharsToWrite)
        {
            cluster = new AllocationUnit();
            //Scrierea efectiva pe 'disk' 
            for (int i = 0; i < noOfCharsToWrite; i++)
            {
                cluster.Content[i] = charGenerator.Next();
            }
        }

    }
}
