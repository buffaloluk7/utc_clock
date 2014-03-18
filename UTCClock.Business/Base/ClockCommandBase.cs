using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business
{
    abstract class ClockCommandBase : ICommand, IUnExecuteableCommand
    {
        public abstract event EventHandler CanExecuteChanged;
        public abstract event EventHandler CanUnExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract bool CanUnExecute(object parameter);

        public abstract void Execute(object parameter);
        public abstract void UnExecute(object parameter);
    }
}
