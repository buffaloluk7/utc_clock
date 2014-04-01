using System;
using System.Text.RegularExpressions;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.Commands
{
    public class SetCommand : IUndoableCommand
    {
        #region Properties

        private string pattern;
        private int newHours;
        private int newMinutes;
        private int newSeconds;
        private DateTime oldTime;

        public string Name
        {
            get { return "set"; }
        }

        public string Description
        {
            get { return "Sets a specific time."; }
        }

        #endregion

        #region Constructors

        public SetCommand()
        {
            this.pattern = "^(?:(?:\\s*-)(?:(?:h\\s+(?<h>[0-9]+))|(?:m\\s+(?<m>[0-9]+))|(?:s\\s+(?<s>[0-9]+)))){1,3}$";
        }

        private SetCommand(int hours, int minutes, int seconds)
        {
            this.newHours = hours;
            this.newMinutes = minutes;
            this.newSeconds = seconds;
        }

        #endregion

        #region IUndoableCommand Implementations

        public ICommand Make(string arguments)
        {
            Match match = Regex.Match(arguments, this.pattern);
            int hours = 0, minutes = 0, seconds = 0;

            int.TryParse(match.Groups["h"].Value, out hours);
            int.TryParse(match.Groups["m"].Value, out minutes);
            int.TryParse(match.Groups["s"].Value, out seconds);

            return new SetCommand(hours, minutes, seconds);
        }

        public bool CanExecute(string arguments)
        {
            return new Regex(this.pattern).Match(arguments).Success;
        }

        public void Execute()
        {
            this.oldTime = ClockModel.Instance.Time;
            ClockModel.Instance.Time = new DateTime(this.oldTime.Year, this.oldTime.Month, this.oldTime.Day, this.newHours, this.newMinutes, this.newSeconds);
        }

        public void UnExecute()
        {
            ClockModel.Instance.Time = this.oldTime;
        }

        #endregion
    }
}
