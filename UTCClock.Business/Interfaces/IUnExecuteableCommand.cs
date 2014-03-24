using System;

namespace UTCClock.Business.Interfaces
{
    public interface IUnExecuteableCommand
    {
        event EventHandler CanUnExecuteChanged;

        bool CanUnExecute(object parameter);

        void UnExecute(object parameter);
    }
}
