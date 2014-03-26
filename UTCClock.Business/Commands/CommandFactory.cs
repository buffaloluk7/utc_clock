using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Commands
{
    public class CommandFactory
    {
        private static readonly CommandFactory instance = new CommandFactory();

        private CommandFactory() { }

        public ICommand createCommand(CommandType type, string input)
        {
            ICommand command = null;
            List<string> splittedInput = input.Split(' ').ToList();
            // das commando selbst aus der liste entfernen
            splittedInput.RemoveAt(0);

            string strRegex = @"(?:(?:[""\-""])(?<param>[""hmsxy""])(?:[""\s""])*(?<value>[""0-9""]*)|(?:[""\-""])(?<param>[""tz""])(?:[""\s""])*(?<value>[""A-z""]*))*";
            MatchCollection matches = Regex.Matches(input, strRegex);

            int h, m, s, x, y;
            string timezone, clockType;

            // set default values
            h = m = s = x = y = 0;
            timezone = "";
            clockType = "";

            switch(type)
            {
                case CommandType.INC:
                case CommandType.DEC:
                case CommandType.SET:
                    foreach (Match match in matches)
                    {
                        switch(match.Groups["param"].Value)
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

                case CommandType.SHOW:
                    foreach (Match match in matches)
                    {
                        switch (match.Groups["param"].Value)
                        {
                            case "x":
                                int.TryParse(match.Groups["value"].Value, out h);
                                break;

                            case "y":
                                int.TryParse(match.Groups["value"].Value, out m);
                                break;

                            case "t":
                                clockType = match.Groups["value"].Value;
                                break;

                            case "z":
                                timezone = match.Groups["value"].Value;
                                break;
                        }
                    }
                    break;
            }

            switch(type)
            {
                case CommandType.INC:
                    command = new IncreaseCommand(h, m, s);
                    break;

                case CommandType.DEC:
                    command = new DecreaseCommand(h, m, s);
                    break;

                case CommandType.SET:
                    command = new SetCommand(h, m, s);
                    break;

                case CommandType.SHOW:
                    command = new ShowCommand();
                    // part von lukas
                    break;
            }

            return command;
        }

        public static CommandFactory Instance 
        {
            get
            {
                return instance;
            }
        }
    }
}
