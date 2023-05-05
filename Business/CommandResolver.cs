using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class CommandResolver
    {
        public static ICommand ResolveCommand(string commandName, List<string> attributes)
        {
            switch (commandName)
            {
                case "dir":
                    return new DirCommand(attributes);

                case "create":
                    return new CreateCommand(attributes);

                default:
                    throw new CommandNotFoundException();
            }
        }
    }
}
