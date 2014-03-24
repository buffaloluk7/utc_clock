using System.ComponentModel;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModels
{
    public class DigitalClock4WindowViewModel : IObserver, INotifyPropertyChanged
    {
        #region Properties

        private readonly ClockModel clock;
        private int hour;
        private int minute;
        private int second;

        public int Hour
        {
            get { return this.hour; }
            private set
            {
                this.hour = value;
                this.RaisePropertyChanged("Hour");
            }
        }

        public int Minute
        {
            get { return this.minute; }
            private set
            {
                this.minute = value;
                this.RaisePropertyChanged("Minute");
            }
        }

        public int Second
        {
            get { return this.second; }
            private set
            {
                this.second = value;
                this.RaisePropertyChanged("Second");
            }
        }

        #endregion

        #region Constructors

        public DigitalClock4WindowViewModel()
        {
            this.clock = ClockModel.Instance;
            this.clock.Subscribe(this);
        }

        #endregion

        #region Implementations

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("DigitalClock 4 received update!");

            this.Hour = this.clock.Time.Hour;
            this.Minute = this.clock.Time.Minute;
            this.Second = this.clock.Time.Second;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
