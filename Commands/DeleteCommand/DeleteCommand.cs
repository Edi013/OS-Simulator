using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class DeleteCommand : ICommand
    {
        public List<string> actualArguments { get; }

        public DeleteCommand(List<string> actualArguments)
        {
            this.actualArguments = actualArguments;
        }

        public void Execute(HWStorage hwStorage)
        {
            WarningMaxNoOfArgs(2);

            string name;
            string extension;
            ParseArguments(out name, out extension);

            CheckIfFileExists(name, hwStorage);

            var element = 
                hwStorage.ROOM.table
                .Where(x => x.name.Equals(name))
                .First()
                .name = "?"; 
        }

        private void CheckIfFileExists(string name, HWStorage storage)
        {
            foreach (var tuple in storage.ROOM.table)
            {
                if (tuple.name == name)
                    return;
            }
            throw new FileDoesNotExistsException($"File {name} doesn't exists.");
        }

        private void ParseArguments(out string name, out string extension)
        {
            if (actualArguments.Count < 1)
                throw new ArgumentNotFoundException("At least one argument of delete command was not found.");

            List<string> nameAndExtension = actualArguments[0].Split(".").ToList();
            name = nameAndExtension[0];
            extension = nameAndExtension[1];

            if (name == null || extension == null || name == "" || extension == ""  || name == " " || extension == " ")
                throw new ArgumentNotFoundException("At least one argument of delete command was not found.");
        }
        public void WarningMaxNoOfArgs(int maxNoOfArgs)
        {
            if (actualArguments.Count > maxNoOfArgs)
                Prompter.NoImplementionForMoreThanNoOfArgs(maxNoOfArgs);
        }
    }
}
