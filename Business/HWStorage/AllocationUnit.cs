using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    [Serializable()]
    public class AllocationUnit
    {
        /*
        Alocation Unit refers to a single cluster.
        A cluster is a piece of storage with chosen clusterSize.
        This OS is form of clusters, the numbers is chosen.
        The choices happen when 'the storage' is formated.
         */

        public char[] Content { get; set; }
        public AllocationUnit()
        {
            int clusterSize = int.Parse(ConfigurationManager.AppSettings["AllocationUnitSize"]);
            int charSize = int.Parse(ConfigurationManager.AppSettings["CharSizeInBytes"]);
            int charCapacity = clusterSize / charSize ; 
            Content = new char[charCapacity];
        }
    }
}
