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
        This table contains chains for each file stored in storage space.
        The size will be the number of clusters.
         */
        int[] table;
        public FAT()
        {
            int size = Int32.Parse(ConfigurationManager.AppSettings["NumberOfClusters"]);
            table = new int[size]; 
        }
    }
}
