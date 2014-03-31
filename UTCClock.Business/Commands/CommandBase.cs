using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UTCClock.Business.Commands
{
    /// <summary>
    /// How to create a new command:
    /// - implement CommandBase
    /// - create a public default constructor and call base.pattern and set your pattern (optional: set all your fields to non-harmful default values)
    /// - create a private constructor, that takes everything your command needs (parsed from the input the user made)
    /// - override the following methods:
    ///     - IsStackable: if you build a command that should be put on the undo/redo stack
    ///     - Execute: do your stuff here
    ///     - UnExecute: undo your stuff here
    ///     - Build: parse the input the user makes and return a ready-to-execute instance of your command
    /// - add an instance of your command to the availableCommand list in the CommandFactory
    /// </summary>
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
