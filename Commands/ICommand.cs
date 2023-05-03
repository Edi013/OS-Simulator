using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public interface ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public List<CommandArgument> Arguments { get; }

        public void Execute(OS os);
    }
}
