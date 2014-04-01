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

        #endregion

        #region Singleton

        public static CommandFactory Instance
        {
            get { return CommandFactory.instance; }
        }

        private CommandFactory()
        {
            CommandFactory.availableCommands = this.getAvailableCommands();
        }

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

        #region Available Commands

        private IList<ICommand> getAvailableCommands()
        {
            return new ICommand[]
            {
                new IncreaseCommand(),
                new DecreaseCommand(),
                new SetCommand(),
                new HelpCommand(),
                new ShowCommand(),
                new ShowAllMacroCommand()
            };
        }

        #endregion
    }
}
