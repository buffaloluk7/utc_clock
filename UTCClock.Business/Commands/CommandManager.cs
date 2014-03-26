using System.Collections.Generic;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class CommandManager
    {
        private readonly static CommandManager instance = new CommandManager();
        private readonly Stack<IStackableCommand> undoCommands = new Stack<IStackableCommand>();
        private readonly Stack<IStackableCommand> redoCommands = new Stack<IStackableCommand>();

        public static CommandManager Instance
        {
            get { return CommandManager.instance; }
        }

        private CommandManager() { }

        public void ExecuteCommand(ICommand command)
        {
            if (command.canExecute())
            {
                redoCommands.Clear();
                command.Execute();

                if (command is IStackableCommand)
                {
                    undoCommands.Push(command as IStackableCommand);
                }
            }
        }

        #region Undo

        public void UndoCommand()
        {
            if (undoCommands.Count == 0)
            {
                return;
            }

            var lastCommand = undoCommands.Pop();
            lastCommand.UnExecute();

            redoCommands.Push(lastCommand);
        }

        #endregion

        #region Redo

        public void RedoCommand()
        {
            IStackableCommand command;

            if (redoCommands.Count == 0)
            {
                if (undoCommands.Count == 0)
                {
                    return;
                }
                command = undoCommands.Peek();
            }
            else
            {
                command = redoCommands.Pop();
            }

            command.Execute();
            undoCommands.Push(command);
        }

        #endregion
    }
}
