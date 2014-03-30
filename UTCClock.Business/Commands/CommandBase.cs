using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UTCClock.Business.Commands
{
    public abstract class CommandBase
    {
        protected string pattern;

        public bool CanExecute(string input)
        {
            return new Regex(pattern).Match(input).Success;
        }

        public virtual bool IsStackable()
        {
            return true;
        }

        public abstract void Execute();

        public abstract void UnExecute();

        public abstract CommandBase Build(string input);
    }
}
