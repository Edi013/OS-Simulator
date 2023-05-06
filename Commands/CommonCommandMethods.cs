using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    public class CommonCommandMethods
    {
        public static void WarningMaxNoOfArgs(int maxNoOfArgs, int argsCount)
        {
            if (argsCount > maxNoOfArgs)
                Prompter.NoImplementionForMoreThanNoOfArgs(maxNoOfArgs);
        }

        public static void ParseArguments(
            out string oldName, out string oldExtension,
            out string newName, out string newExtension,
            List<string> args)
        {
            //Used in rename, copy commands
            if (args.Count < 2)
                throw new ArgumentNotFoundException("At least one argument of rename command was not found.");

            var oldNameAndExtension = args[0].Split(".").ToList();
            var newNameAndExtension = args[1].Split(".").ToList();

            oldName = oldNameAndExtension[0];
            oldExtension = oldNameAndExtension[1];
            newName = newNameAndExtension[0];
            newExtension = newNameAndExtension[1];

            if (oldName == null || newName == null ||
                oldName == "" || newName == "" ||
                oldName == " " || newName == " " ||
                oldExtension == null || newExtension == null ||
                oldExtension == "" || newExtension == "" ||
                oldExtension == " " || newExtension == " ")
                throw new ArgumentNotFoundException("At least one argument of delete command was not found.");
        }
        public static RoomTuple CheckIfFileExists(string name, string extension, HWStorage storage)
        {
            foreach (var tuple in storage.ROOM.table)
            {
                if (tuple.name == name && tuple.extension == extension)
                    return tuple;
            }
            throw new FileDoesNotExistsException($"File {name} doesn't exists.");
        }

        public static List<ushort> AllocateResourcesForNewFile
            (HWStorage storage, string fileName, string fileExtension, int sizeInBytes)
        {
            CheckForFreeEntryInRoom(storage);
            CheckForFreeAllocantionChainInFat(storage, sizeInBytes);
            List<ushort> allocationChain = AllocateChainInFat(storage, sizeInBytes);
            AllocateEntryInRoom(storage, fileName, fileExtension, (ushort)sizeInBytes, allocationChain[0]);

            return allocationChain;
        }
        private static void CheckForFreeEntryInRoom(HWStorage storage)
        {
            if (!storage.ROOM.ExistsFreeEntry())
                throw new RoomIsFullException();
        }
        private static void CheckForFreeAllocantionChainInFat(HWStorage storage, int sizeInBytes)
        {
            if (!storage.FAT.CheckFreeAllocationChain(sizeInBytes))
                throw new FatIsFullException();
        }
        private static List<ushort> AllocateChainInFat(HWStorage storage, int sizeInBytes)
        {
            //alocate the allocation chain for a file in fat
            //and save the cluseters to write the file 
            return
                storage.FAT.AllocateChainForFile(sizeInBytes);
        }
        private static void AllocateEntryInRoom
            (HWStorage storage, string fileName, string fileExtension,
             ushort sizeInBytes, ushort FAU)
        {
            //get the first free entry in room 
            ushort indexOfFreeEntryInRoom = (ushort)storage.ROOM.GetFirstFreeEntry();

            //build and add the tuple in room
            RoomTuple newRoomTuple =
                new RoomTuple(fileName, fileExtension, sizeInBytes, FAU);

            storage.ROOM.AddTupleInRoom(indexOfFreeEntryInRoom, newRoomTuple);
        }
    }
}
