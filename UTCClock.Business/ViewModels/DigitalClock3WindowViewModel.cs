using System;
using System.ComponentModel;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModels
{
    public class DigitalClock3WindowViewModel : IObserver
    {
        #region Properties

        private readonly ClockModel clock;
        private readonly TimeSpan timeZoneOffset;
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

        public DigitalClock3WindowViewModel(TimeSpan timeZoneOffset)
        {
            this.timeZoneOffset = timeZoneOffset;
            this.clock = ClockModel.Instance;
            this.clock.Subscribe(this);
        }

        #endregion

        #region IObserver Implementations

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("DigitalClock 3 received update!");

            DateTimeOffset dateTime = new DateTimeOffset(this.clock.Time).ToOffset(this.timeZoneOffset);
            new DateTimeOffset(DateTime.UtcNow, new TimeSpan(3,0,0));

            this.Hour = dateTime.Hour;
            this.Minute = dateTime.Minute;
            this.Second = dateTime.Second;
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
