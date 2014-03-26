using System;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.Commands
{
    public class IncreaseCommand : IStackableCommand
    {
        #region Properties

        private int hours;
        private int minutes;
        private int seconds;

        #endregion

        #region Constructors

        public IncreaseCommand(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        #endregion

        #region IStackableCommand Implementations

        public bool CanExecute()
        {
            return (hours >= 0 && minutes >= 0 && seconds >= 0 && minutes < 60 && seconds < 60);
        }

        public void Execute()
        {
            TimeSpan t = new TimeSpan(hours, minutes, seconds);
            ClockModel.Instance.Time = ClockModel.Instance.Time.Add(t);
        }

        public void UnExecute()
        {
            TimeSpan t = new TimeSpan(hours, minutes, seconds);
            ClockModel.Instance.Time = ClockModel.Instance.Time.Subtract(t);
        }

        #endregion
    }
}
