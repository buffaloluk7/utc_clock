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

        private CommandFactory() { }

        public static CommandFactory Instance 
        {
            get
            {
                return instance;
            }
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

            List<string> splittedInput = input.Split(' ').ToList();
            // Entferne das Command aus der Liste
            splittedInput.RemoveAt(0);

            string strRegex = @"(?:(?:[""\-""])(?<param>[""hmsxy""])(?:[""\s""])*(?<value>[""0-9""]*)|(?:[""\-""])(?<param>[""tz""])(?:[""\s""])*(?<value>[""A-z""]*))*";
            MatchCollection matches = Regex.Matches(input, strRegex);

            // Setze Default-Werte
            h = m = s = default(int);
            x = y = default(double);
            clockType = default(ClockType);
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
                                this.parseTimeZoneName(match.Groups["value"].Value, out timeZone);
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
        
        private void parseTimeZoneName(string timeZoneName, out TimeSpan timeZoneOffset)
        {
            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
            string strRegex = @"(?:\(UTC(?:[""\+\-""][""0-9""]{2}:[""0-9""]{2}){0,}\)\s)(.*)";
            Regex myRegex = new Regex(strRegex, RegexOptions.IgnoreCase);
            timeZoneOffset = new TimeSpan();

            foreach (var timeZone in timeZones)
            {
                foreach (Match match in myRegex.Matches(timeZoneName))
                {
                    if (match.Success)
                    {
                        List<string> cities = match.Groups["name"].Value.Split(',').ToList();
                        if (cities.Contains(timeZoneName))
                        {
                            timeZoneOffset = timeZone.BaseUtcOffset;
                            return;
                        }
                    }
                }
            }
        }
    }
}
