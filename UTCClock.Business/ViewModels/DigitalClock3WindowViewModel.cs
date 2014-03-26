using System.ComponentModel;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModels
{
    public class DigitalClock3WindowViewModel : IObserver
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
                this.OnPropertyChanged("Hour");
            }
        }

        public int Minute
        {
            get { return this.minute; }
            private set
            {
                this.minute = value;
                this.OnPropertyChanged("Minute");
            }
        }

        public int Second
        {
            get { return this.second; }
            private set
            {
                this.second = value;
                this.OnPropertyChanged("Second");
            }
        }

        #endregion

        #region Constructors

        public DigitalClock3WindowViewModel()
        {
            this.clock = ClockModel.Instance;
            this.clock.Subscribe(this);
        }

        #endregion

        #region IObserver Implementations

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("DigitalClock 3 received update!");

            this.Hour = this.clock.Time.Hour;
            this.Minute = this.clock.Time.Minute;
            this.Second = this.clock.Time.Second;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
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
