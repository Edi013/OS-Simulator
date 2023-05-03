namespace PrivateOS.Business
{
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
        public bool attribute;

        public RoomTuple(string name, string extension, ushort size, ushort firstAllocationUnit)
        {
            this.name = name;
            this.extension = extension;
            this.size = size;
            this.firstAllocationUnit = firstAllocationUnit;
            attribute = false;
        }
        public RoomTuple(string name, string extension, ushort size, ushort firstAllocationUnit, bool attribute)
        {
            this.name = name;
            this.extension = extension;
            this.size = size;
            this.firstAllocationUnit = firstAllocationUnit;
            this.attribute = attribute;
        }

        public override string ToString()
        {
            return $"Name: {name} | Extension: {extension}| Size: {size} | FAU:{firstAllocationUnit}";
        }
    }
}
