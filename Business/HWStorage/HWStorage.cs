using PrivateOS.DataAccess;
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
        public AllocationUnit GetStorageCluster(ushort indexFromFat)
        {
            var indexFromStorage = FromFatIndexToStorageIndex(indexFromFat);
            return GetCluster(indexFromStorage);
        }
        private AllocationUnit GetCluster(int indexFromStorage)
        {
            return Storage[indexFromStorage];
        }
        private int FromFatIndexToStorageIndex(ushort indexFromFat)
        {
            // 4096 clusters
            // 0   ... 511  = FAT     (512)
            // 512 ... 576  = ROOM    (64)
            // 576 ... 4095 = STORAGE (3520)
            // 0 ... 3519 - AllocationUnit[] Storage

            return indexFromFat 
                - ushort.Parse(ConfigurationManager.AppSettings["FATSize"])
                - ushort.Parse(ConfigurationManager.AppSettings["ROOMSize"]);
        }
    }
}
