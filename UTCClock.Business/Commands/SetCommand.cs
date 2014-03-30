using System;
using System.Text.RegularExpressions;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.Commands
{
    class SetCommand : CommandBase
    {
        #region Properties

        private int hours;
        private int minutes;
        private int seconds;
        private DateTime old_time;

        #endregion

        #region Constructors

        public SetCommand()
        {
            // initialize pattern 
            base.pattern = @"^(?:set)(?:(?:\s-)(?:(?:h\s+(?<h>[0-9]+))|(?:m\s+(?<m>[0-9]+))|(?:s\s+(?<s>[0-9]+)))){1,3}$";

            // set non-harming default values
            hours = default(int);
            minutes = default(int);
            seconds = default(int);
        }

        private SetCommand(int hours, int minutes, int seconds)
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

            return new SetCommand(hours, minutes, seconds);
        }

        #region Implementations

        public override void Execute()
        {
            old_time = ClockModel.Instance.Time;
            DateTime new_time = new DateTime(old_time.Year, old_time.Month, old_time.Day, hours, minutes, seconds);

            ClockModel.Instance.Time = new_time;
        }

        public override void UnExecute()
        {
            ClockModel.Instance.Time = old_time;
        }

        #endregion
    }
}
