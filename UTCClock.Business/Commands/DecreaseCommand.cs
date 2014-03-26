using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.Commands
{
    public class DecreaseCommand : IStackableCommand
    {
        private int hours;
        private int minutes;
        private int seconds;

        public DecreaseCommand(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public bool canExecute()
        {
            return (hours >= 0 && minutes >= 0 && seconds >= 0 && minutes < 60 && seconds < 60);
        }

        public void Execute()
        {
            TimeSpan t = new TimeSpan(hours, minutes, seconds);
            ClockModel.Instance.Time = ClockModel.Instance.Time.Subtract(t);
        }

        public void UnExecute()
        {
            TimeSpan t = new TimeSpan(hours, minutes, seconds);
            ClockModel.Instance.Time = ClockModel.Instance.Time.Add(t);
        }
    }
}
