using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public class HWStorage
    {
        public FAT FAT { get; set; }
        public ROOM ROOM { get; set; }
        public List<AllocationUnit> Storage { get; set; }

        public HWStorage()
        {
            this.FAT = new FAT();
            this.ROOM = new ROOM();
            this.Storage = new List<AllocationUnit>();
        }
        
        // read FAT, ROOM and Storage from DB, eventually
    }
}
