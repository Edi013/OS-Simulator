using System.Configuration;

namespace PrivateOS.Business
{
    [Serializable()]
    public class HWStorage
    {
        public FAT FAT { get; set; }
        public ROOM ROOM { get; set; }
        public AllocationUnit[] Storage { get; set; }

        public HWStorage()
        {
            this.FAT = new FAT();
            this.ROOM = new ROOM();
            int usableClusters =
                ushort.Parse(ConfigurationManager.AppSettings["NumberOfClusters"]) -
                ushort.Parse(ConfigurationManager.AppSettings["FATSize"]) -
                ushort.Parse(ConfigurationManager.AppSettings["ROOMSize"]);

            this.Storage = new AllocationUnit[usableClusters];
        }
        
        // read FAT, ROOM and Storage from DB, eventually
    }
}
