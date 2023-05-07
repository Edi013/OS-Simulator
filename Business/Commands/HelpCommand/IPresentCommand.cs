using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public interface IPresentCommand
    {
        public string Name { get; }
        public string Description { get; }
        public string Structure { get; }
        public List<CommandArgument> Arguments { get; }
    }
}
