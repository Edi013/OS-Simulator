using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    [Serializable()]
    public class ROOM
    {
        /*
         This class represents a table of tuples.
         Those are used to locate and rebuild a file stored in _ HWStorage
         */
        public RoomTuple[] table { get; }

        public ROOM()
        {
            ushort size = ushort.Parse(System.Configuration.ConfigurationManager.AppSettings["ROOMSize"]);
            table = new
                RoomTuple[size];
        }

        public bool ExistsFreeEntry()
        {
            for (short i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].name == "?")
                {
                    return true;
                }
            }
            return false;
        }
        public ushort GetFirstFreeEntry()
        {
            for (ushort i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].name == "?")
                    return i;
            }
            throw new Exception();
        }
        public void AddTupleInRoom(ushort index, RoomTuple roomTuple)
        {
            table[index] = roomTuple;
        }
    }
}