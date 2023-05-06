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
        public AllocationUnit CreateStorageCluster(int indexFromFat)
        {

            int indexFromStorage = FromFatIndexToStorageIndex(indexFromFat);
            Storage[indexFromStorage] = new AllocationUnit();
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
        public void CopyChainValuesIntoGivenChain (List<ushort> allocationChainToCopy, List<ushort> allocationChainToCopyTo)
        {
            /*
             Avand doua lanturi de alocare din fat, unul se copiaza in celalalt:

             Pentru fiecare cluster:
                -- Transformam indexii din tabla FAT, pentru acesarea structurii Storage din clasa HWStorage
                    --- Transformarea se face atat pentru clusterele de copiat, cat si pentru cele in care se copiaza.
                -- Accesam fiecare AU din Storage de copiat
                -- Fiecare caracter se copiaza in AU corespunzatoare lantului de alocare al noului fisier.
             */
            for (int noClustersToCopy = 0; noClustersToCopy < allocationChainToCopy.Count; noClustersToCopy++)
            {
                int storageIndexOfClusterToCopy = FromFatIndexToStorageIndex(allocationChainToCopy[noClustersToCopy]); 
                int storageIndexOfClusterToCopyTo = FromFatIndexToStorageIndex(allocationChainToCopyTo[noClustersToCopy]); 
                for (int j = 0; j <  Storage[storageIndexOfClusterToCopy].Content.Length; j++)
                {
                    Storage[storageIndexOfClusterToCopyTo].Content[j] =
                        Storage[storageIndexOfClusterToCopy].Content[j];
                }
            }
        }
    }
}
