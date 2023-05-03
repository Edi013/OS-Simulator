using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class CommandResolver
    {
        public static Type ResolveCommand(string commandName)
        {
            switch (commandName)
            {
                case "dir":
                    return typeof(DirCommand);

                case "create":
                    return typeof(CreateCommand);

                default:
                    throw new CommandNotFound();
            }
        }
    }
}
