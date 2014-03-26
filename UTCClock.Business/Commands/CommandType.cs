using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTCClock.Business.Commands
{
    public enum CommandType
    {
        UNDO,
        REDO,
        SET,
        SHOW,
        INC,
        DEC,
        EXIT
    }
}
