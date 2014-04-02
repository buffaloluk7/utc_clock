using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;
using UTCClock.Business.ViewModels;

namespace UTCClock.Business.Commands
{
    public class ShowCommand : ICommand
    {
        #region Properties

        private static Dictionary<IEnumerable<string>, TimeSpan> timeZonesDictionary;
        private double? x;
        private double? y;
        private string pattern;
        private ClockType clockType;
        private TimeSpan timeZone;

        public string Name
        {
            get { return "show"; }
        }

        public string Description
        {
            get { return "Open a new window with a given clock type."; }
        }

        #endregion

        #region Constructors

        static ShowCommand()
        {
            ShowCommand.timeZonesDictionary = ShowCommand.parseTimeZones();
        }

        public ShowCommand()
        {
            this.pattern = "^(?:(?:\\s*-)(?:(?:t\\s+(?<t>[A-z]+))|(?:z\\s+(?<z>[A-z]+))|(?:x\\s+(?<x>[0-9]+))|(?:y\\s+(?<y>[0-9]+)))){0,4}$";
        }

        private ShowCommand(ClockType clockType, TimeSpan timeZone, double? x = null, double? y = null)
        {
            this.clockType = clockType;
            this.timeZone = timeZone;
            this.x = x;
            this.y = y;
        }

        #endregion

        #region ICommand Implementations

        public ICommand Make(string arguments)
        {
            Match match = Regex.Match(arguments, this.pattern);
            ClockType clockType = ClockType.None;
            TimeSpan timeZoneOffet = new TimeSpan();
            double x, y;

            Enum.TryParse<ClockType>(match.Groups["t"].Value, true, out clockType);
            this.TryParseTimeZone(match.Groups["z"].Value, out timeZoneOffet);

            if (!double.TryParse(match.Groups["x"].Value, out x) || !double.TryParse(match.Groups["y"].Value, out y))
            {
                return new ShowCommand(clockType, timeZoneOffet);
            }

            return new ShowCommand(clockType, timeZoneOffet, x, y);
        }

        public bool CanExecute(string arguments)
        {
            return new Regex(this.pattern).Match(arguments).Success;
        }

        public void Execute()
        {
            switch (this.clockType)
            {
                case ClockType.None:
                    this.navigate<BeigeClockWindowViewModel>();
                    break;

                case ClockType.Blue:
                    this.navigate<BlueClockWindowViewModel>();
                    break;

                case ClockType.Beige:
                    this.navigate<BeigeClockWindowViewModel>();
                    break;

                case ClockType.Coral:
                    this.navigate<CoralClockWindowViewModel>();
                    break;

                case ClockType.Grey:
                    this.navigate<GreyClockWindowViewModel>();
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void navigate<T>()
        {
            if (x.HasValue && y.HasValue)
            {
                ViewModelLocator.NavigationService.Navigate<T>(this.x.Value, this.y.Value, this.timeZone);   
            }
            else
            {
                ViewModelLocator.NavigationService.Navigate<T>(this.timeZone);
            }
        }

        private void TryParseTimeZone(string timeZoneName, out TimeSpan timeZoneOffet)
        {
            timeZoneOffet = new TimeSpan();

            if (string.IsNullOrWhiteSpace(timeZoneName))
            {
                return;
            }

            timeZoneName = timeZoneName.ToLower();

            foreach (KeyValuePair<IEnumerable<string>, TimeSpan> keyValuePair in ShowCommand.timeZonesDictionary)
            {
                if (Enumerable.Contains<string>(keyValuePair.Key, timeZoneName))
                {
                    timeZoneOffet = keyValuePair.Value;
                    return;
                }
            }
        }

        private static Dictionary<IEnumerable<string>, TimeSpan> parseTimeZones()
        {
            Dictionary<IEnumerable<string>, TimeSpan> timeZonesDictionary = new Dictionary<IEnumerable<string>, TimeSpan>();
            ReadOnlyCollection<TimeZoneInfo> systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            Regex regex = new Regex("(?:\\(UTC(?:[\"\\+\\-\"][\"0-9\"]{2}:[\"0-9\"]{2}){0,}\\)\\s)(?<name>.*)", RegexOptions.IgnoreCase);

            foreach (TimeZoneInfo timeZoneInfo in systemTimeZones)
            {
                foreach (Match match in regex.Matches(timeZoneInfo.DisplayName))
                {
                    if (match.Success)
                    {
                        IEnumerable<string> timeZoneCity = match.Groups["name"].Value.Split(new string[] { ", " }, StringSplitOptions.None).ToList().ConvertAll(t => t.ToLower());
                        timeZonesDictionary.Add(timeZoneCity, timeZoneInfo.BaseUtcOffset);
                    }
                }
            }

            return timeZonesDictionary;
        }

        #endregion
    }
}
