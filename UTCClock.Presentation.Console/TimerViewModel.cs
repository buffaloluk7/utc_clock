using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UTCClock.Business.Model;

namespace UTCClock.Presentation.Console
{
    class TimerViewModel
    {
        private Clock clock = Clock.Instance;
        private Timer timer = new Timer();

        public TimerViewModel()
        {
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
        }

        public void Start()
        {
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            clock.Time = clock.Time.AddSeconds(1);
        }

    }
}
