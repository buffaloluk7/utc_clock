using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class CommandManager
    {
        #region Properties

        private readonly static CommandManager instance = new CommandManager();
        private readonly Stack<IStackableCommand> undoCommands = new Stack<IStackableCommand>();
        private readonly Stack<IStackableCommand> redoCommands = new Stack<IStackableCommand>();

        public static CommandManager Instance
        {
            get { return CommandManager.instance; }
        }

        #endregion

        #region Constructor

        private CommandManager() { }

        #endregion

        #region Execute

        public void ExecuteCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (command.CanExecute())
            {
                redoCommands.Clear();
                command.Execute();

                if (command is IStackableCommand)
                {
                    undoCommands.Push(command as IStackableCommand);
                }
            }
        }

        #endregion

        #region Undo

        public void UndoCommand()
        {
            if (undoCommands.Count == 0)
            {
                MessageBox.Show("Nichts rückgängig zu machen.");
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
                    MessageBox.Show("Nichts zu wiederholen.");
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
