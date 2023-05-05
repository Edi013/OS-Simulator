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

                case "help":
                    return new HelpCommand(attributes);
                case "\" \"":
                    return new ShutDownCommand(attributes);
                case "delete":
                    return new DeleteCommand(attributes);
                case "rename":
                    return new RenameCommand(attributes);

                default:
                    throw new CommandNotFoundException();
            }
        }
    }
}
