using System;
using System.Text.RegularExpressions;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.Commands
{
    public class DecreaseCommand : CommandBase
    {
        #region Properties

        private int hours;
        private int minutes;
        private int seconds;

        #endregion

        #region Constructors

        public DecreaseCommand()
        {
            // initialize pattern 
            base.pattern = @"^(?:dec)(?:(?:\s-)(?:(?:h\s+(?<h>[0-9]+))|(?:m\s+(?<m>[0-9]+))|(?:s\s+(?<s>[0-9]+)))){1,3}$";

            // set non-harming default values
            hours = default(int);
            minutes = default(int);
            seconds = default(int);
        }
        private DecreaseCommand(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        #endregion

        public override CommandBase Build(string input)
        {
            var match = Regex.Match(input, base.pattern);

            int.TryParse(match.Groups["h"].Value, out hours);
            int.TryParse(match.Groups["m"].Value, out minutes);
            int.TryParse(match.Groups["s"].Value, out seconds);

            return new DecreaseCommand(hours, minutes, seconds);
        }

        #region Implementations

        public override void Execute()
        {
            TimeSpan t = new TimeSpan(hours, minutes, seconds);
            ClockModel.Instance.Time = ClockModel.Instance.Time.Subtract(t);
        }

        public override void UnExecute()
        {
            TimeSpan t = new TimeSpan(hours, minutes, seconds);
            ClockModel.Instance.Time = ClockModel.Instance.Time.Add(t);
        }

        #endregion
    }
}
