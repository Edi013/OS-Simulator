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
        public AllocationUnit GetClusterHavingFatIndex(int indexFromFat)
        {

            int indexFromStorage = FromFatIndexToStorageIndex(indexFromFat);
            return Storage[indexFromStorage];
        }
        public int FromFatIndexToStorageIndex(int indexFromFat)
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
                if (allocationChainToCopy[noClustersToCopy] == 0)
                    break;
                int storageIndexOfClusterToCopy = FromFatIndexToStorageIndex(allocationChainToCopy[noClustersToCopy]); 
                
                int storageIndexOfClusterToCopyTo = FromFatIndexToStorageIndex(allocationChainToCopyTo[noClustersToCopy]);
               
                int clusterSizeInChars = 
                    int.Parse(ConfigurationManager.AppSettings["AllocationUnitSize"]) / int.Parse(ConfigurationManager.AppSettings["CharSizeInBytes"]);
                for (int j = 0; j < clusterSizeInChars; j++) // Storage[storageIndexOfClusterToCopy].Content.Length
                {
                    Storage[storageIndexOfClusterToCopyTo].Content[j] =
                        Storage[storageIndexOfClusterToCopy].Content[j];
                }
            }
        }

        public void InitialiazeClusters(List<ushort> allocationChainFromFatToCopyTo)
        {
            for (int i = 0; i < allocationChainFromFatToCopyTo.Count; i++)
            {
                int storageIndexOfClusterToCopyTo = FromFatIndexToStorageIndex(allocationChainFromFatToCopyTo[i]);
                CreateCluster(storageIndexOfClusterToCopyTo);
            }
        }
        private void CreateCluster(int indexFromStorage)
        {
            Storage[indexFromStorage] = new AllocationUnit();
        }

        public void DeleteClusters(List<ushort> allocationChainFromFatToCopyTo)
        {
            for (int cluster = 0; cluster < allocationChainFromFatToCopyTo.Count; cluster++)
            {
                int storageIndexOfClusterToCopyTo = FromFatIndexToStorageIndex(allocationChainFromFatToCopyTo[cluster]);
                DeleteCluster(storageIndexOfClusterToCopyTo);
            }
        }
        private void DeleteCluster(int indexFromStorage)
        {
            Storage[indexFromStorage] = null;
        }
    }
}
