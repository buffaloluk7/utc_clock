using System;
using System.Collections.Generic;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class CommandFactory
    {
        #region Properties

        private static readonly CommandFactory instance = new CommandFactory();
        private static IList<ICommand> availableCommands;

        public IList<ICommand> AvailableCommands
        {
            get { return CommandFactory.availableCommands; }
        }

        public static CommandFactory Instance
        {
            get { return CommandFactory.instance; }
        }

        #endregion

        #region Constructors

        static CommandFactory()
        {
            CommandFactory.availableCommands = new ICommand[]
            {
                new IncreaseCommand(),
                new DecreaseCommand(),
                new SetCommand(),
                new HelpCommand(),
                new ShowCommand(),
                new ShowAllMacroCommand()
            };
        }

        private CommandFactory() { }

        #endregion

        #region Create Command

        public ICommand CreateCommand(string commandName, string commandArguments = "")
        {
            if (string.IsNullOrWhiteSpace(commandName))
            {
                throw new ArgumentException("commandName");
            }
                
            foreach (ICommand command in CommandFactory.availableCommands)
            {
                if (command.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase) && command.CanExecute(commandArguments))
                {
                    return command.Make(commandArguments);
                }
            }

            throw new NotImplementedException("Command does not exist.");
        }

        #endregion
    }
}
