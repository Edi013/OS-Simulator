﻿namespace PrivateOS.Business
{
    [Serializable()]
    public class RoomTuple
    {
        /* 
        RoomTuple is the basic unit used in ROOM table
          
        This class should describe a tuple of 3 parameters who's purpose is 
        to outline / demarcate a chain of Allocation Units that forms a file.
        The chain should be restored using FileAllocationTable.
         */
        public string name;
        public string extension;
        public ushort size;
        public ushort firstAllocationUnit;
        public bool flag;

        public RoomTuple(string name, string extension, ushort size, ushort firstAllocationUnit)
        {
            this.name = name;
            this.extension = extension;
            this.size = size;
            this.firstAllocationUnit = firstAllocationUnit;
            flag = false;
        }
        public RoomTuple(string name, string extension, ushort size, ushort firstAllocationUnit, bool attribute)
        {
            this.name = name;
            this.extension = extension;
            this.size = size;
            this.firstAllocationUnit = firstAllocationUnit;
            this.flag = attribute;
        }

        public override string ToString()
        {
            return $"{name}.{extension} {size} bytes  FAU:{firstAllocationUnit}";
        }
        public string DisplayMinimalDetails()
        {
            return $"{name}.{extension}";
        }
        public string DisplayAllDetails()
        {
            if (flag)
                return $"{ToString()} | Flag:{flag}";
            return ToString();
        }
    }
}
