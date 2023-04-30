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
         Those are used to locate and rebuild a file stored in ore Storage
         */
        RoomTuple[] list;

        public ROOM()
        {
            var size = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["ROOMSize"]);
            list = new
                RoomTuple[size];
        }

    }
}
