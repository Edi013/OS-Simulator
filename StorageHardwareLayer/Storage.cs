using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public class Storage
    {
        public FAT FAT { get; set; }
        public ROOM ROOM { get; set; }
        public List<AllocationUnit> StorageClusters { get; set; }
    }
}
