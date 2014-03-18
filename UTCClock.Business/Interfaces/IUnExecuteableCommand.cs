using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UTCClock.Business.Interfaces
{
    public interface IUnExecuteableCommand
    {
        event EventHandler CanUnExecuteChanged;

        bool CanUnExecute(object parameter);
        void UnExecute(object parameter);
    }
}
