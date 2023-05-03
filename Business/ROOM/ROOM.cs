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
        public void UpdatePosition(ushort index, RoomTuple value)
        {
            table[index] = value;
        }

    }
}
