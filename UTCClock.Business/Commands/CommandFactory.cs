using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Common;
using System.Collections.ObjectModel;

namespace UTCClock.Business.Commands
{
    public class CommandFactory
    {
        private static readonly CommandFactory instance = new CommandFactory();
        private IList<CommandBase> availableCommands;

        private CommandFactory()
        {
            // here we could load command from a dll or something like that
            availableCommands = new CommandBase[] 
            {
                new IncreaseCommand(),
                new DecreaseCommand(),
                new SetCommand(),
                new HelpCommand(),
                new ShowCommand(),
                new ShowAllMacroCommand()
            };
        }

        public static CommandFactory Instance 
        {
            get { return instance; }
        }

        public CommandBase CreateCommand(string input)
        {
            CommandBase newCommand = null;

            foreach (var command in availableCommands)
            {
                if (command.CanExecute(input))
                {
                    newCommand = command.Build(input);
                }
            }

            if (newCommand == null)
            {
                throw new NotImplementedException();
            }

            return newCommand;
        }
    }
}
