using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModel
{
    public class DigitalClockWindowViewModel : ViewModelBase, IObserver
    {
        public DigitalClockWindowViewModel()
        {
            Clock.Instance.Subscribe(this);
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public void Update(IObservable subject)
        {
            System.Diagnostics.Debug.WriteLine("Recieved update!");

            this.Hour = Clock.Instance.Time.Hour;
            this.Minute = Clock.Instance.Time.Minute;
            this.Second = Clock.Instance.Time.Second;
        }
    }
}
