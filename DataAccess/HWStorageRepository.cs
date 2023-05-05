using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using PrivateOS.Business;

namespace PrivateOS.DataAccess
{
    public class HWStorageRepository : IHWStorageRepository
    {
        private string FilePath { get; set; }
        
        public HWStorageRepository()
        {
            FilePath = ConfigurationManager.AppSettings["dbFilePath"];
            if (FilePath == null || FilePath == "")
                throw new Exception("Repository error. Path to db-file is null or empty.");
        }

        public void SaveStorage(HWStorage storage)
        {
            Serialize(storage);
        }
        public HWStorage GetStorage()
        {
            return Deserialize();
        }

        private HWStorage Deserialize()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                    {
                        if (fs.Length == 0)
                            return new HWStorage();

                        BinaryFormatter formatter = new BinaryFormatter();
                        HWStorage result;
                        
                        return (HWStorage)formatter.Deserialize(fs);
                    };
                }
                using (FileStream fs = new FileStream(FilePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return new HWStorage();
                };
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }
        private void Serialize(HWStorage storage)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, storage);
                }
                catch (Exception)
                {
                    throw;
                }

                return;
            };
        }
    }
}
