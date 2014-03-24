using System;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class HelpCommand : ICommandBase
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanUnExecuteChanged;

        public bool CanUnExecute(object parameter)
        {
            return false;
        }

        public void UnExecute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
