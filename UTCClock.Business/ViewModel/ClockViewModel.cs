using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModel
{
    class ClockViewModel
    {
        private Clock clock = Clock.Instance;

        public DateTime Time
        {
            get
            {
                return clock.Time;
            }

            set
            {
                clock.Time = value;
            }
        }
    }
}
