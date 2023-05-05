namespace PrivateOS.Business
{
    public class RenameCommand : ICommand
    {
        public List<string> actualArguments { get; }

        public RenameCommand(List<string> actualArguments)
        {
            this.actualArguments = actualArguments;
        }

        public void Execute(HWStorage hwStorage)
        {
            WarningMaxNoOfArgs(2);

            string oldName;
            string oldExtension;
            string newName;
            string newExtension;
            ParseArguments(out oldName, out oldExtension, out newName, out newExtension);

            CheckIfFileExists(oldName, oldExtension, hwStorage);

            RoomTuple element =
                hwStorage.ROOM.table
                .Where(x => x.name.Equals(oldName) &&
                            x.extension.Equals(oldExtension))
                .First();
            element.name = newName;
            element.extension = newExtension;
        }

        private void CheckIfFileExists(string name, string extension, HWStorage storage)
        {
            foreach (var tuple in storage.ROOM.table)
            {
                if (tuple.name == name && tuple.extension == extension)
                    return;
            }
            throw new FileDoesNotExistsException($"File {name} doesn't exists.");
        }

        private void ParseArguments(
            out string oldName, out string oldExtension,
            out string newName, out string newExtension)
        {
            if (actualArguments.Count < 2)
                throw new ArgumentNotFoundException("At least one argument of rename command was not found.");

            var oldNameAndExtension = actualArguments[0].Split(".").ToList();
            var newNameAndExtension = actualArguments[1].Split(".").ToList();

            oldName = oldNameAndExtension[0];
            oldExtension = oldNameAndExtension[1];
            newName = newNameAndExtension[0];
            newExtension = newNameAndExtension[1];

            if (oldName == null || newName == null ||
                oldName == "" || newName == ""  ||
                oldName == " " || newName == " " ||
                oldExtension == null || newExtension == null || 
                oldExtension == "" || newExtension == "" ||
                oldExtension == " " || newExtension == " ")
                throw new ArgumentNotFoundException("At least one argument of delete command was not found.");
        }
        public void WarningMaxNoOfArgs(int maxNoOfArgs)
        {
            if (actualArguments.Count > maxNoOfArgs)
                Prompter.NoImplementionForMoreThanNoOfArgs(maxNoOfArgs);
        }
    }
}
