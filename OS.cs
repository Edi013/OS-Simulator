using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public class OS
    {
        public FAT fat { get; set; }
        public ROOM room { get; set; }
        public HWStorage Storage { get; private set; } 


        public OS()
        {
            Storage = new HWStorage();
            fat = new FAT();
            room = new ROOM();
        }

        public void UpdateFat()
        {
            Storage.FAT.Update(fat);
        }
        public void UpdateRoom()
        {
            Storage.ROOM.Update(room);
        }
        public void UpdateTables()
        {
            UpdateFat();
            UpdateRoom();
        }
    }
}
