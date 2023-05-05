using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public class AllocationUnit
    {
        /*
        Alocation Unit refers to a single cluster.
        A cluster is a piece of storage with chosen size.
        This OS is form of clusters, the numbers is chosen.
        The choices happen when 'the storage' is formated.
         */

        public char[] Content { get; set; }
        public AllocationUnit()
        {
            ushort size = ushort.Parse(ConfigurationManager.AppSettings["AllocationUnitSize"]);
            Content = new char[size];
        }
    }
}
