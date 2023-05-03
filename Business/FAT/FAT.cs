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

        public FAT()
        {
            ushort noOfClusters = ushort.Parse(ConfigurationManager.AppSettings["NumberOfClusters"]);
            table = new ushort[noOfClusters];

            ushort fatSize = ushort.Parse(ConfigurationManager.AppSettings["FATSize"]);
            for (ushort i = 0; i < noOfClusters; i++)
            {
                if (i < fatSize)
                    table[i] = ReservedCluster;
                else
                    table[i] = UnusedCluster;
            }
        }

        public void UpdatePosition(ushort index, ushort value)
        {
            table[index] = value;
        }
    }
}
