using System.Configuration;

namespace PrivateOS.Business
{
    public class CreateCommand : ICommand
    {
        public List<string> actualArguments { get; }
        private Iterator charGenerator { get; set; }
        private string[] contentTypes;

        public CreateCommand(List<string> actualArguments)
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
            // Parsam 3 argumente, 4 variabile
            // Daca nu exista resurse pentru alocare -> throw (early return)
            // --mecanismul de creare--
            try
            {
                CommonCommandMethods.WarningMaxNoOfArgs(3, actualArguments.Count);

                string fileName, fileExtension, contentType;
                ushort sizeInBytes;

                ParseArguments(storage, out fileName, out fileExtension, out sizeInBytes, out contentType);

                List<ushort> allocationChainFromFat 
                    = CommonCommandMethods
                        .AllocateResourcesForNewFile
                        (storage, fileName, fileExtension, sizeInBytes);

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
                    (double.Parse(ConfigurationManager.AppSettings["RoomEntrySizeOfExtension"]) / double.Parse(ConfigurationManager.AppSettings["CharSizeInBytes"]));
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

            foreach (var item in contentTypes)
            {
                if (item.Equals(contentType))
                    return;
            }   
            throw new ArgumentNotValidException("Content type asked in create command is not valid!");
        }

        private void WriteFile(HWStorage storage, List<ushort> allocationChainFromFat, int fileSizeByCharSize, int clusterCharCapacity)
        {
            // Incepem scriere pe disc.
            // Initializam clusterele
            // Pentru fiecare cluster - (*) obtinem clusterul avand indexul din FAT 
            //                        - (**) daca nu e ultimul, scriem capacitatea maxima admisa.
            //                        - (***) daca  e ultimul, scriem diferenta mai jos calculata.

            storage.InitialiazeClusters(allocationChainFromFat);
            
            for (int clusterNo = 0; clusterNo < allocationChainFromFat.Count; clusterNo++)
            {
                // (*)
                var allocationUnitFromStorage = 
                    storage.GetClusterHavingFatIndex(
                        allocationChainFromFat[clusterNo]);

                // (**) 
                if (clusterNo == allocationChainFromFat.Count - 1)
                {
                    int remainingNoOfChars = fileSizeByCharSize - (clusterNo * clusterCharCapacity);
                    WriteCluster(allocationUnitFromStorage, remainingNoOfChars);
                    break;
                }
                // (***)
                WriteCluster(allocationUnitFromStorage, clusterCharCapacity);
            }
        }
        private void WriteCluster(AllocationUnit cluster, int noOfCharsToWrite)
        {
            //cluster = new AllocationUnit();
            //Scrierea efectiva pe 'disk' 
            for (int i = 0; i < noOfCharsToWrite; i++)
            {
                var character = charGenerator.Next();
                cluster.Content[i] = character;
            }
        }

    }
}
