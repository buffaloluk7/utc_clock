using System.Collections.Generic;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class ShowAllMacroCommand : ICommand
    {
        #region Properties

        public string Name
        {
            get { return "showall"; }
        }

        public string Description
        {
            get { return "Show multiple windows with different clock types."; }
        }

        #endregion

        #region ICommand Implementations

        public ICommand Make(string arguments)
        {
            return new ShowAllMacroCommand();
        }

        public bool CanExecute(string arguments)
        {
            return true;
        }

        public void Execute()
        {
            IList<ICommand> commands = new ICommand[]
            {
                CommandFactory.Instance.CreateCommand("show", "-t blue -z wien -x 100 -y 100"),
                CommandFactory.Instance.CreateCommand("show", "-t coral -z london -x 100 -y 500"),
                CommandFactory.Instance.CreateCommand("show", "-t grey -z hawaii -x 500 -y 100"),
                CommandFactory.Instance.CreateCommand("show", "-t beige -z peking -x 500 -y 500")
            };

            foreach (ICommand command in commands)
            {
                CommandManager.Instance.ExecuteCommand(command);
            }
        }

        #endregion
    }
}
