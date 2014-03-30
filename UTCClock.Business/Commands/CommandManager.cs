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
        private readonly Stack<CommandBase> undoCommands = new Stack<CommandBase>();
        private readonly Stack<CommandBase> redoCommands = new Stack<CommandBase>();

        public static CommandManager Instance
        {
            get { return CommandManager.instance; }
        }

        #endregion

        #region Constructor

        private CommandManager() { }

        #endregion

        #region Execute

        public void ExecuteCommand(CommandBase command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            redoCommands.Clear();
            command.Execute();

            if (command.IsStackable())
            {
                undoCommands.Push(command);
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
            CommandBase command;

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
