using PrivateOS.Business;

namespace PrivateOS.DataAccess
{
    public interface IHWStorageRepository
    {
        public void SaveStorage(HWStorage storage);
        public HWStorage GetStorage();

/*        public void SaveFAT(FAT fat);
        public void SaveROOM(ROOM room);*/
    }
}
