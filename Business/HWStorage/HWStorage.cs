using PrivateOS.DataAccess;
using System.Configuration;

namespace PrivateOS.Business
{
    [Serializable()]
    public class HWStorage
    {
        public FAT FAT { get; set; }
        public ROOM ROOM { get; set; }
        //56300 e numarul maxim de bytes pe care ne lasa sa il scriem in storage in loc de 56320
        public AllocationUnit[] Storage { get; set; }

        public HWStorage()
        {
            this.FAT = new FAT();
            this.ROOM = new ROOM();

            int usableClusters =
                int.Parse(ConfigurationManager.AppSettings["NumberOfClusters"]) -
                int.Parse(ConfigurationManager.AppSettings["FATSize"]) -
                int.Parse(ConfigurationManager.AppSettings["ROOMSize"]);
            this.Storage = new AllocationUnit[usableClusters];
        }
        public AllocationUnit GetStorageCluster(int indexFromFat)
        {
            int indexFromStorage = FromFatIndexToStorageIndex(indexFromFat);
            return GetCluster(indexFromStorage);
        }
        private AllocationUnit GetCluster(int indexFromStorage)
        {
            return Storage[indexFromStorage];
        }
        private int FromFatIndexToStorageIndex(int indexFromFat)
        {
            // 4096 clusters
            // 0   ... 511  = FAT     (512)
            // 512 ... 576  = ROOM    (64)
            // 576 ... 4095 = STORAGE (3520)
            // 0 ... 3519 - AllocationUnit[] Storage

            return indexFromFat 
                - int.Parse(ConfigurationManager.AppSettings["FATSize"])
                - int.Parse(ConfigurationManager.AppSettings["ROOMSize"]);
        }
    }
}
