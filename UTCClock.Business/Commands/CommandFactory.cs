using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            // https://stackoverflow.com/questions/491595/best-way-to-parse-command-line-arguments-in-c
            ICommand command;

            switch(type)
            {
                case CommandType.INC:
                    command = new IncreaseCommand(0, 0, 0);
                    break;

                case CommandType.DEC:
                    command = new DecreaseCommand(0, 0, 0);
                    break;

                case CommandType.SET:
                    command = new SetCommand(0, 0, 0);
                    break;
                
                    // should never happen
                default:
                    command = null;
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
