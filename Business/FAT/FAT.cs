using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    [Serializable()]
    public class FAT
    {
        /*
            File Allocation Table 
        This table contains chains (pointers) for each file stored in storage space.
        The noOfClusters will be the number of clusters.
         */


        //UInt16 = UnsignedInt16 should be used because numbers greater than 255 need 2 bytes ( octeti ) to be stored 
        // https://learn.microsoft.com/en-us/dotnet/standard/numerics
        public ushort[] table;


        // The first FATSize+ROOMSize clusters can't be used.
        // They already contain FAT and ROOM tables
        // Therefore, we can use FAT's occupied indexes as flags:
        // e.g. 0 - end of linked list
        //      1 - bad cluster ( damaged cluster )
        public static ushort EndOfChain = 0;
        public static ushort BadCluster = 1;
        public static ushort ReservedCluster = 2;
        public static ushort UnusedCluster = 3;
        public static ushort InitState = 4;


        public ushort allocationUnitSize;

        public FAT()
        {
            allocationUnitSize = ushort.Parse(ConfigurationManager.AppSettings["AllocationUnitSize"]);

            ushort noOfClusters = ushort.Parse(ConfigurationManager.AppSettings["NumberOfClusters"]);
            table = new ushort[noOfClusters];

            int fatSize = int.Parse(ConfigurationManager.AppSettings["FATSize"]);
            int roomSize = int.Parse(ConfigurationManager.AppSettings["ROOMSize"]);
            for (ushort i = 0; i < noOfClusters; i++)
            {
                if (i < fatSize + roomSize)
                    table[i] = ReservedCluster;
                else
                    table[i] = UnusedCluster;
            }
        }

        public List<ushort> ReadChain(ushort indexOfFAU)
        {
            List<ushort> chain = new List<ushort>();

            ushort indexOfNextCluster = indexOfFAU;
            chain.Add(indexOfNextCluster);

            while (table[indexOfNextCluster] != 2)
            {
                indexOfNextCluster = table[indexOfNextCluster];
                chain.Add(indexOfNextCluster);
            }

            return chain;
        }
        public bool CheckFreeAllocationChain(int fileSize)
        {
            int noOfClustersNeeded = CalculateNumberOfClustersNeeded(fileSize);
            int numberOfClustersFound = 0;

            for (int j = 0; j < table.Length; j++)
            {
                if (table[j] == UnusedCluster)
                {
                    numberOfClustersFound++;
                    if (numberOfClustersFound == noOfClustersNeeded)
                        return true;
                }
            }
            return false;
            
        }
        public List<ushort> AllocateChainForFile(int fileSize)
        {
            int noOfClustersNeeded = CalculateNumberOfClustersNeeded(fileSize);
            int numberOfClustersFound = 0;

            //used just for initialization as a boolean type that works as an ok)
            ushort lastClusterIndex = InitState;
            List<ushort> allocationUnits = new List<ushort>();
            for (ushort currentIndex = 0; currentIndex < table.Length; currentIndex++)
            {
                if (table[currentIndex] == 3)
                {
                    allocationUnits.Add(currentIndex);

                    //doar daca a fost gasita prima unitate libera
                    //din cea gasita precedent aratam spre indexul urmatoarei
                    if (lastClusterIndex != InitState)
                        table[lastClusterIndex] = currentIndex;
                        

                    lastClusterIndex = currentIndex;
                    numberOfClustersFound++;

                    if (numberOfClustersFound == noOfClustersNeeded)
                        break;
                }
            }
            table[lastClusterIndex] = EndOfChain;

            return allocationUnits;
        }
        private int CalculateNumberOfClustersNeeded(int fileSize)
        {
            int noOfClustersNeeded = 0;
            while (fileSize > 0)
            {
                noOfClustersNeeded++;
                fileSize -= allocationUnitSize;
            }
            return noOfClustersNeeded;
        }
        public List<ushort> IdentifyAllocationChainBasedOnFAU(int FAU)
        {
            if (FAU == null || FAU < 575)
                throw new Exception("Index was null or <575 accesing FAT.");

            ushort currentValue = (ushort)FAU;
            List<ushort> allocationChain = new List<ushort>();
            allocationChain.Add(currentValue);

            while (table[currentValue] != 0)
            {
                currentValue = table[currentValue];
                allocationChain.Add(currentValue);
            }
            return allocationChain;
        }
        public void DeleteAllocationChainWithFAU(List<ushort> allocationChainToDelete)
        {
            for (int cluster = 0; cluster < allocationChainToDelete.Count; cluster++)
            {
                table[allocationChainToDelete[cluster]] = FAT.UnusedCluster;
            }
        }
    }
}

//create max files
//implement presentation in help for delete and rename