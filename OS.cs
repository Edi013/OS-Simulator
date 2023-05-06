using PrivateOS.Business;
using PrivateOS.DataAccess;

namespace PrivateOS
{
    public class OS
    {
        private HWStorage Storage { get; } 
        private Prompter Prompter { get; set; }
        private IHWStorageRepository Repository { get; set; }

        public OS()
        {
            Prompter = new Prompter();
            Repository = new HWStorageRepository();
            Storage = Repository.GetStorage();
        }

        public void Run()
        {
            Prompter.WelcomeUser();

            while (true)
            {
                Prompter.BeginNewLine();
                try // Sistem centralizat pentru error handling.
                {
                    //Preluam comanda de la user
                    string userInput = Prompter.AskForCommand();

                    //Impartim comanda in doua: nume comanda, argumente
                    string commandName;
                    List<string> arguments;
                    SplitCommand(userInput, out commandName, out arguments);

                    //Cerem 'gestionarului de dependinte' sa ne dea comanda dorita
                    var commandInstance = CommandResolver.ResolveCommand(commandName, arguments);
                    ICommand command = commandInstance;
                    commandInstance.Execute(Storage);

                    if(command is CopyCommand || 
                       command is DeleteCommand ||
                       command is RenameCommand)
                    {
                        Repository.SaveStorage(Storage);
                    }
                }
                catch (ShutDownException e)
                {
                    Console.WriteLine(e.Message);
                    if (Prompter.WantsToExit())
                        break;
                }
                catch (CommandNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try 'help' command.");
                }
                catch (ArgumentNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try 'help' command.");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try 'help' command.");
                }
                catch (ArgumentNotValidException e) when (e is ArgumentNotValidException)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try 'help' command.");
                }
                catch (CreateCommandParametersMissingException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try 'help' command.");
                }
                catch (FatIsFullException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (RoomIsFullException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FileNameTakenException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (NameIsTooLongException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(FileDoesNotExistsException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error happend, message: \n" + e.Message + "\n" + e.StackTrace);
                    Console.WriteLine("Try 'help' command.");
                }
                finally
                {
                    Console.WriteLine();
                }
            }
        }
        private static void SplitCommand(string command, out string commandName, out List<string> arguments)
        {
            string[] splittedCommand = command.ToLower().Split(" ");

            commandName = splittedCommand.First().ToLower();
            arguments = splittedCommand.Skip(1).ToList();
        }
    }
}
