using System;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.Commands
{
    class SetCommand : IStackableCommand
    {
        #region Properties

        private int new_hours;
        private int new_minutes;
        private int new_seconds;
        private DateTime old_time;

        #endregion

        #region Constructors

        public SetCommand(int hours, int minutes, int seconds)
        {
            this.new_hours = hours;
            this.new_minutes = minutes;
            this.new_seconds = seconds;
        }

        #endregion

        #region IStackableCommand Implementations

        public bool CanExecute()
        {
            return (new_hours >= 0 && new_minutes >= 0 && new_seconds >= 0 && new_minutes < 60 && new_seconds < 60);
        }

        public void Execute()
        {
            old_time = ClockModel.Instance.Time;
            DateTime new_time = new DateTime(old_time.Year, old_time.Month, old_time.Day, new_hours, new_minutes, new_seconds);

            ClockModel.Instance.Time = new_time;
        }

        public void UnExecute()
        {
            ClockModel.Instance.Time = old_time;
        }

        #endregion
    }
}
