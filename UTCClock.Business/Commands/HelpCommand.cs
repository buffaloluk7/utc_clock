using System.Text;
using System.Windows.Forms;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class HelpCommand : ICommand
    {
        #region Properties

        public string Name
        {
            get { return "help"; }
        }

        public string Description
        {
            get { return "HelpCommand displays some useful information."; }
        }

        #endregion

        #region ICommand Implementations

        public ICommand Make(string arguments)
        {
            return new HelpCommand();
        }

        public bool CanExecute(string arguments)
        {
            return true;
        }

        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Willkommen bei der Hilfe. Ich hoffe ich konnte Dir helfen.");
            sb.AppendLine();

            foreach (var command in CommandFactory.Instance.AvailableCommands)
            {
                sb.Append(command.Name);
                sb.Append("\t");
                sb.AppendLine(command.Description);
            }

            MessageBox.Show(sb.ToString(), "Hilfe");
        }

        #endregion
    }
}
