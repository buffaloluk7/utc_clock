using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTCClock.Business
{
    class CommandExecuter
    {
        private readonly static CommandExecuter instance = new CommandExecuter();
        private readonly Stack<KeyValuePair<ClockCommandBase, object>> undoCommands = new Stack<KeyValuePair<ClockCommandBase, object>>();
        private readonly Stack<KeyValuePair<ClockCommandBase, object>> redoCommands = new Stack<KeyValuePair<ClockCommandBase, object>>();

        public static CommandExecuter Instance
        {
            get { return CommandExecuter.instance; }
        }

        private CommandExecuter() { }

        void ExecuteCommand(ClockCommandBase command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                redoCommands.Clear();
                command.Execute(parameter);
                undoCommands.Push(new KeyValuePair<ClockCommandBase, object>(command, parameter));
            }
        }

        void UndoCommand()
        {
            if (undoCommands.Count == 0)
            {
                return;
            }

            var lastCommand = undoCommands.Pop();
            lastCommand.Key.Execute(lastCommand.Value);

            redoCommands.Push(lastCommand);
        }

        void RedoCommand()
        {
            if (redoCommands.Count == 0)
            {
                return;
            }

            var lastCommand = redoCommands.Pop();
            lastCommand.Key.UnExecute(lastCommand.Value);

            undoCommands.Push(lastCommand);
        }
    }
}
