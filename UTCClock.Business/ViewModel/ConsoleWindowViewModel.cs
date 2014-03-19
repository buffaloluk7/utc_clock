using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModel
{
    public class ConsoleWindowViewModel : ViewModelBase
    {
        ClockViewModel clockViewModel = new ClockViewModel();
        Timer timer = new Timer();

        public ConsoleWindowViewModel()
        {
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            clockViewModel.Time = clockViewModel.Time.AddSeconds(1);
        }
    }
}
