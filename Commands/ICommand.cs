using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public interface ICommand
    {
        public List<string> actualArguments { get; }
        public void Execute(HWStorage hwStorage);
    }
}
