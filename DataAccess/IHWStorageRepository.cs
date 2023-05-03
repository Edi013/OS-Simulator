using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.DataAccess
{
    public interface IHWStorageRepository
    {
        public void WriteStorage(HWStorage storage);
        public HWStorage ReadStorage();

    }
}
