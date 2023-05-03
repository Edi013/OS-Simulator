using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;

namespace PrivateOS.DataAccess
{
    public class HWStorageRepository : IHWStorageRepository
    {
        private string filePath;
        
        public HWStorageRepository()
        {
            filePath = ConfigurationManager.AppSettings["dbFilePath"];
        }

        public HWStorage ReadStorage()
        {
            return Deserialize();
        }
        public void WriteStorage(HWStorage storage)
        {
            Serialize(storage);
        }

        private HWStorage Deserialize()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    if(fs.Length == 0)
                        return new HWStorage();

                    BinaryFormatter formatter = new BinaryFormatter();
                    HWStorage result;
                    try
                    {
                        result = (HWStorage)formatter.Deserialize(fs);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    return result;
                };
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return new HWStorage();
            };
        }
        private void Serialize(HWStorage storage)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
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
