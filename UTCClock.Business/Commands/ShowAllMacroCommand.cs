using System.Collections.Generic;
using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class ShowAllMacroCommand : ICommand
    {
        public ShowAllMacroCommand() { }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            List<ICommand> commands = new List<ICommand>();

            commands.Add(CommandFactory.Instance.CreateCommand(CommandType.Show, "show -t blue -z wien -x 100 -y 100"));
            commands.Add(CommandFactory.Instance.CreateCommand(CommandType.Show, "show -t coral -z london -x 100 -y 500"));
            commands.Add(CommandFactory.Instance.CreateCommand(CommandType.Show, "show -t grey -z hawaii -x 500 -y 100"));
            commands.Add(CommandFactory.Instance.CreateCommand(CommandType.Show, "show -t beige -z peking -x 500 -y 500"));

            foreach (var command in commands)
            {
                CommandManager.Instance.ExecuteCommand(command);   
            }
        }
    }
}
