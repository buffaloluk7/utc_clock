using System.Collections.Generic;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class CommandManager
    {
        private readonly static CommandManager instance = new CommandManager();
        private readonly Stack<KeyValuePair<IStackableCommand, object>> undoCommands = new Stack<KeyValuePair<IStackableCommand, object>>();
        private readonly Stack<KeyValuePair<IStackableCommand, object>> redoCommands = new Stack<KeyValuePair<IStackableCommand, object>>();

        public static CommandManager Instance
        {
            get { return CommandManager.instance; }
        }

        private CommandManager() { }

        public void ExecuteCommand(ICommand command, object argument)
        {
            if (command.canExecute(argument))
            {
                redoCommands.Clear();
                command.Execute(argument);

                if (command is IStackableCommand)
                {
                    undoCommands.Push(new KeyValuePair<IStackableCommand, object>(command as IStackableCommand, argument));
                }
            }
        }

        public void UndoCommand()
        {
            if (undoCommands.Count == 0)
            {
                return;
            }

            var lastCommand = undoCommands.Pop();
            lastCommand.Key.Execute(lastCommand.Value);

            redoCommands.Push(lastCommand);
        }

        public void RedoCommand()
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
