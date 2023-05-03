using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public class FAT
    {
        /*
            File Allocation Table 
        This table contains chains (pointers) for each file stored in storage space.
        The size will be the number of clusters.
         */


        //UInt16 = UnsignedInt16 should be used because numbers greater than 255 need 2 bytes ( octeti ) to be stored 
        // https://learn.microsoft.com/en-us/dotnet/standard/numerics
        System.UInt16[] table;


        // The first FATSize+ROOMSize clusters can't be used.
        // They already contain FAT and ROOM tables
        // Therefore, we can use FAT's occupied indexes as flags:
        // e.g. 0 - end of linked list
        //      1 - bad cluster ( damaged cluster )
        public static int EndOfChain = 0;
        public static int BadCluster = 1;
        public static int ReservedCluster = 2;

        public FAT()
        {
            int size = Int32.Parse(ConfigurationManager.AppSettings["NumberOfClusters"]);
            table = new System.UInt16[size];

            for (int i = 0; i < Int32.Parse(ConfigurationManager.AppSettings["FATSize"]); i++)
                table[i] = 2;
        }

        public void Update(FAT fat)
        {
            for(int i = 0; i < table.Length; i++)
                table[i] = fat.table[i];
        }
    }
}
