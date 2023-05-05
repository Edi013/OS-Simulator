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
            int size = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ROOMSize"]);
            table = new
                RoomTuple[size];
        }

        public bool ExistsFreeEntry()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].name == "?")
                {
                    return true;
                }
            }
            return false;
        }
        public int GetFirstFreeEntry()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].name == "?")
                    return i;
            }
            throw new Exception();
        }
        public void AddTupleInRoom(int index, RoomTuple roomTuple)
        {
            table[index] = roomTuple;
        }
    }
}