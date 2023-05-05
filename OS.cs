using PrivateOS.Business;
using PrivateOS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    string command = Prompter.AskForCommand();
                    ValidateCommand(command);

                    //Impartim comanda in doua: nume comanda, argumente
                    string commandName;
                    List<string> arguments;
                    SplitCommand(command, out commandName, out arguments);

                    //Cerem 'gestionarului de dependinte' sa ne dea comanda dorita
                    var commandInstance = CommandResolver.ResolveCommand(commandName, arguments);
                    ICommand comand = commandInstance;
                    commandInstance.Execute(Storage);

                    Repository.SaveStorage(Storage);
                }
                catch (CancelCommandException e)
                {
                    Console.WriteLine(e.Message);
                    if (Prompter.WantsToExit())
                        break;
                }
                catch (CommandNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentNotValidException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(CreateCommandParametersMissingException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error happend, message: \n" + e.Message + "\n" + e.StackTrace);
                    throw;
                }
                finally
                {
                    Console.WriteLine();
                }
            }
        }
        private static void ValidateCommand(string command)
        {
            if (command == null || command == "")
                throw new CommandNotFoundException();
            if (command == " ")
                throw new CancelCommandException();
        }
        private static void SplitCommand(string command, out string commandName, out List<string> arguments)
        {
            string[] splittedCommand = command.ToLower().Split(" ");

            commandName = splittedCommand.First();
            arguments = splittedCommand.Skip(1).ToList();
        }
    }
}
