using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS
{
    public interface ICommand
    {
        public string Name { get; }
        public string Description { get; }

        public void Execute(OS os);
    }
}
