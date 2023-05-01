using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public class ROOM
    {
        /*
         This class represents a table of tuples.
         Those are used to locate and rebuild a file stored in _ HWStorage
         */
        RoomTuple[] table;

        public ROOM()
        {
            var size = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["ROOMSize"]);
            table = new
                RoomTuple[size];
        }

        public void Update(ROOM room)
        {
            for (int i = 0; i < table.Length; i++)
                table[i] = room.table[i];
        }

    }
}
