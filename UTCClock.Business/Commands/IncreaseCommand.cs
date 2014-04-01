using System;
using System.Text.RegularExpressions;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.Commands
{
    public class IncreaseCommand : IUndoableCommand
    {
        #region Properties

        private string pattern;
        private int hours;
        private int minutes;
        private int seconds;

        public string Name
        {
            get { return "inc"; }
        }

        public string Description
        {
            get { return "Increase the time."; }
        }

        #endregion

        #region Constructors

        public IncreaseCommand()
        {
            this.pattern = "^(?:(?:\\s*-)(?:(?:h\\s+(?<h>[0-9]+))|(?:m\\s+(?<m>[0-9]+))|(?:s\\s+(?<s>[0-9]+)))){1,3}$";
        }

        private IncreaseCommand(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
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

            return new IncreaseCommand(hours, minutes, seconds);
        }

        public bool CanExecute(string arguments)
        {
            return new Regex(this.pattern).Match(arguments).Success;
        }

        public void Execute()
        {
            ClockModel.Instance.Time = ClockModel.Instance.Time.Add(new TimeSpan(this.hours, this.minutes, this.seconds));
        }

        public void UnExecute()
        {
            ClockModel.Instance.Time = ClockModel.Instance.Time.Subtract(new TimeSpan(this.hours, this.minutes, this.seconds));
        }

        #endregion
    }
}
