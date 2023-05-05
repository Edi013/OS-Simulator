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
            /*
            Room size in clusters = 32

            Room entry size in clusters = 2
            Room entry caracteristics :
                name = 18 bytes
                extension = 8 bytes
                size = 2 bytes
                FUA = 2 bytes
                flags/attributes = 2 bytes

            This is because a char size is 2bytes.
            So the extension needs at least 4 chars x 2 = 8 bytes 
            & the name would be nice to have more then 4 chars.
             */

            int roomSizeInClusters = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ROOMSize"]);
            int noOfRoomTuple = (int)Math.Floor(
                (double)roomSizeInClusters / double.Parse(System.Configuration.ConfigurationManager.AppSettings["RoomEntrySize"]));
            
            table = new
                RoomTuple[noOfRoomTuple];
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