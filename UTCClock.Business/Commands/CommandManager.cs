using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class CommandManager
    {
        #region Properties

        private static readonly CommandManager instance = new CommandManager();
        private readonly Stack<IUndoableCommand> undoCommands = new Stack<IUndoableCommand>();
        private readonly Stack<IUndoableCommand> redoCommands = new Stack<IUndoableCommand>();

        public static CommandManager Instance
        {
            get { return CommandManager.instance; }
        }

        #endregion

        #region Constructors

        private CommandManager() { }

        #endregion

        #region Execute Command

        public void ExecuteCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            this.redoCommands.Clear();
            command.Execute();

            if (command is IUndoableCommand)
            {
                this.undoCommands.Push(command as IUndoableCommand);
            }
        }

        #endregion

        #region Undo Command

        public void UndoCommand()
        {
            if (this.undoCommands.Count == 0)
            {
                MessageBox.Show("Nichts rückgängig zu machen.");
            }
            else
            {
                IUndoableCommand undoableCommand = this.undoCommands.Pop();
                undoableCommand.UnExecute();
                this.redoCommands.Push(undoableCommand);
            }
        }

        #endregion

        #region Redo Command

        public void RedoCommand()
        {
            IUndoableCommand undoableCommand;

            if (this.redoCommands.Count == 0)
            {
                if (this.undoCommands.Count == 0)
                {
                    MessageBox.Show("Nichts zu wiederholen.");
                    return;
                }
                else
                {
                    undoableCommand = this.undoCommands.Peek();
                }
            }
            else
            {
                undoableCommand = this.redoCommands.Pop();
            }

            undoableCommand.Execute();
            this.undoCommands.Push(undoableCommand);
        }

        #endregion
    }
}
