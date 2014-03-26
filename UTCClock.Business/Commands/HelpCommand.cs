using System.Windows.Forms;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class HelpCommand : ICommand
    {
        #region ICommand Implementations

        public bool CanExecute()
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
