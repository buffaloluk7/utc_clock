using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;
using UTCClock.Business.ViewModels;
using System.Collections.ObjectModel;

namespace UTCClock.Business.Commands
{
    class ShowCommand : CommandBase
    {
        #region Properties

        private double x;
        private double y;
        private ClockType clockType;
        private TimeSpan timeZone;

        private Dictionary<IEnumerable<string>, TimeSpan> timeZonesDictionary = new Dictionary<IEnumerable<string>, TimeSpan>();

        #endregion

        #region Constructors

        public ShowCommand()
        {
            base.pattern = @"^(?:show)(?:(?:\s-)(?:(?:t\s+(?<t>[A-z]+))|(?:z\s+(?<z>[A-z]+))|(?:x\s+(?<x>[0-9]+))|(?:y\s+(?<y>[0-9]+)))){0,4}$";

            x = default(double);
            y = default(double);
            clockType = ClockType.None;
            timeZone = new TimeSpan(0, 0, 0);

            this.parseTimeZones();
        }

        private ShowCommand(ClockType clockType, TimeSpan timeZone, double x, double y)
        {
            this.clockType = clockType;
            this.timeZone = timeZone;
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Implementations
        public override CommandBase Build(string input)
        {
            var match = Regex.Match(input, base.pattern);

            double.TryParse(match.Groups["x"].Value, out this.x);
            double.TryParse(match.Groups["y"].Value, out this.y);
            Enum.TryParse(match.Groups["s"].Value, out clockType);
            this.TryParseTimeZone(match.Groups["t"].Value, out this.timeZone);

            return new ShowCommand(clockType, timeZone, x, y);
        }

        public override void Execute()
        {
            switch (this.clockType)
            {
                case ClockType.Beige:
                    this.navigate<BeigeClockWindowViewModel>();
                    break;

                case ClockType.Blue:
                    this.navigate<BlueClockWindowViewModel>();
                    break;

                case ClockType.Coral:
                    this.navigate<CoralClockWindowViewModel>();
                    break;

                case ClockType.Grey:
                    this.navigate<GreyClockWindowViewModel>();
                    break;

                // ClockType not set - ClockType.NONE
                default:
                    this.navigate<BeigeClockWindowViewModel>();
                    break;
            }
        }

        public override void UnExecute()
        {
            return;
        }
        public override bool IsStackable()
        {
            return false;
        }

        #endregion

        #region Private Methods

        private void navigate<T>()
        {
            if (x < 0 && y < 0)
            {
                ViewModelLocator.NavigationService.Navigate<T>(this.timeZone);
            }
            else
            {
                ViewModelLocator.NavigationService.Navigate<T>(this.timeZone, null, x, y);
            }
        }

        private void TryParseTimeZone(string timeZoneName, out TimeSpan timeZoneOffet)
        {
            timeZoneOffet = new TimeSpan();
            timeZoneName = timeZoneName.ToLower();

            foreach (var tz in this.timeZonesDictionary)
            {
                if (tz.Key.Contains(timeZoneName))
                {
                    timeZoneOffet = tz.Value;
                    return;
                }
            }
        }

        private void parseTimeZones()
        {
            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
            string strRegex = @"(?:\(UTC(?:[""\+\-""][""0-9""]{2}:[""0-9""]{2}){0,}\)\s)(?<name>.*)";
            Regex myRegex = new Regex(strRegex, RegexOptions.IgnoreCase);

            foreach (var timeZone in timeZones)
            {
                foreach (Match match in myRegex.Matches(timeZone.DisplayName))
                {
                    if (match.Success)
                    {
                        IEnumerable<string> timeZoneCity = match.Groups["name"].Value.Split(new string[] { ", " }, StringSplitOptions.None).ToList().ConvertAll(t => t.ToLower());
                        this.timeZonesDictionary.Add(timeZoneCity, timeZone.BaseUtcOffset);
                    }
                }
            }
        }

        #endregion
    }
}
