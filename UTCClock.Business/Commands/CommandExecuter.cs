using System.Collections.Generic;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business
{
    public class CommandExecuter
    {
        private readonly static CommandExecuter instance = new CommandExecuter();
        private readonly Stack<KeyValuePair<ICommandBase, object>> undoCommands = new Stack<KeyValuePair<ICommandBase, object>>();
        private readonly Stack<KeyValuePair<ICommandBase, object>> redoCommands = new Stack<KeyValuePair<ICommandBase, object>>();

        public static CommandExecuter Instance
        {
            get { return CommandExecuter.instance; }
        }

        private CommandExecuter() { }

        void ExecuteCommand(ICommandBase command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                redoCommands.Clear();
                command.Execute(parameter);
                undoCommands.Push(new KeyValuePair<ICommandBase, object>(command, parameter));
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
