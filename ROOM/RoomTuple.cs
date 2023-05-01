using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public class RoomTuple
    {
        /* 
        RoomTuple is the basic unit used in ROOM table
          
        This class should describe a tuple of 3 parameters who's purpose is 
        to outline / demarcate a chain of Allocation Units that forms a file.
        The chain should be restored using FileAllocationTable.
         */
        string Name { get; set; }
        public int Size { get; set; }
        public int FirstAllocationUnit { get; set; }

        public RoomTuple(string name, int size, int firstAllocationUnit)
        {
            Name = name;
            Size = size;
            FirstAllocationUnit = firstAllocationUnit;
        }

        public override string ToString()
        {
            return $"Name: {Name} | Size: {Size} | FAU:{FirstAllocationUnit}";
        }
    }
}
