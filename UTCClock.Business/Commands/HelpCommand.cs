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
            MessageBox.Show("Willkommen bei der Hilfe. Ich hoffe ich konnte Dir helfen.", "Hilfe");
        }

        #endregion
    }
}
