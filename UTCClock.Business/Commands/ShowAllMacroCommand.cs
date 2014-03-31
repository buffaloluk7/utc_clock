using System.Collections.Generic;
using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class ShowAllMacroCommand : CommandBase
    {
        public ShowAllMacroCommand() 
        {
            base.pattern = @"^(?:showall)$";
        }

        public override CommandBase Build(string input)
        {
            return new ShowAllMacroCommand();
        }

        public override void Execute()
        {
            List<CommandBase> commands = new List<CommandBase>();

            commands.Add(CommandFactory.Instance.CreateCommand("show -t blue -z wien -x 100 -y 100"));
            commands.Add(CommandFactory.Instance.CreateCommand("show -t coral -z london -x 100 -y 500"));
            commands.Add(CommandFactory.Instance.CreateCommand("show -t grey -z hawaii -x 500 -y 100"));
            commands.Add(CommandFactory.Instance.CreateCommand("show -t beige -z peking -x 500 -y 500"));

            foreach (var command in commands)
            {
                CommandManager.Instance.ExecuteCommand(command);   
            }
        }

        public override void UnExecute()
        {
            return;
        }

        public override bool IsStackable()
        {
            return false;
        }
    }
}
