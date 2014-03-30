using System.Windows.Forms;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class HelpCommand : CommandBase
    {
        #region Implementations

        public HelpCommand()
        {
            base.pattern = @"^(?:help)$";
        }

        public override void Execute()
        {
            MessageBox.Show("Willkommen bei der Hilfe. Ich hoffe ich konnte Dir helfen.", "Hilfe");
        }

        public override void UnExecute()
        {
            return;
        }

        public override bool IsStackable()
        {
            return false;
        }

        public override CommandBase Build(string input)
        {
            return new HelpCommand();
        }

        #endregion
    }
}
