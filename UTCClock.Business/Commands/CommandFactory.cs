using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Common;
using System.Collections.ObjectModel;

namespace UTCClock.Business.Commands
{
    public class CommandFactory
    {
        private static readonly CommandFactory instance = new CommandFactory();
        private Dictionary<IEnumerable<string>, TimeSpan> timeZonesDictionary = new Dictionary<IEnumerable<string>,TimeSpan>();

        private CommandFactory()
        {
            this.parseTimeZones();
        }

        public static CommandFactory Instance 
        {
            get { return instance; }
        }

        public ICommand CreateCommand(CommandType type, string input)
        {
            if (type == CommandType.None)
            {
                throw new ArgumentNullException("type");
            }

            ICommand command = null;
            int h, m, s;
            double x, y;
            TimeSpan timeZone;
            ClockType clockType;

            string strRegex = @"(?:(?:[""\-""])(?<param>[""hmsxy""])(?:[""\s""])*(?<value>[""0-9""]*)|(?:[""\-""])(?<param>[""tz""])(?:[""\s""])*(?<value>[""A-z""]*))*";
            MatchCollection matches = Regex.Matches(input, strRegex);

            // Setze Default-Werte
            h = m = s = 0;
            x = y = -1;
            clockType = ClockType.None;
            timeZone = new TimeSpan(0, 0, 0);

            switch (type)
            {
                case CommandType.Inc:
                case CommandType.Dec:
                case CommandType.Set:
                    foreach (Match match in matches)
                    {
                        switch (match.Groups["param"].Value)
                        {
                            case "h":
                                int.TryParse(match.Groups["value"].Value, out h);
                                break;

                            case "m":
                                int.TryParse(match.Groups["value"].Value, out m);
                                break;

                            case "s":
                                int.TryParse(match.Groups["value"].Value, out s);
                                break;
                        }
                    }
                    break;

                case CommandType.Show:
                    foreach (Match match in matches)
                    {
                        switch (match.Groups["param"].Value)
                        {
                            case "x":
                                double.TryParse(match.Groups["value"].Value, out x);
                                break;

                            case "y":
                                double.TryParse(match.Groups["value"].Value, out y);
                                break;

                            case "t":
                                Enum.TryParse<ClockType>(match.Groups["value"].Value, true, out clockType);
                                break;

                            case "z":
                                this.TryParseTimeZone(match.Groups["value"].Value, out timeZone);
                                break;
                        }
                    }
                    break;
            }

            switch (type)
            {
                case CommandType.Help:
                    command = new HelpCommand();
                    break;

                case CommandType.Inc:
                    command = new IncreaseCommand(h, m, s);
                    break;

                case CommandType.Dec:
                    command = new DecreaseCommand(h, m, s);
                    break;

                case CommandType.Set:
                    command = new SetCommand(h, m, s);
                    break;

                case CommandType.Show:
                    command = new ShowCommand(clockType, timeZone, x, y);
                    break;
            }

            return command;
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
    }
}
