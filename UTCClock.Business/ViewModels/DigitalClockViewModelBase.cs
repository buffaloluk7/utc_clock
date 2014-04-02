using System;
using UTCClock.Business.Common;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;
using Luvi.Json.Extension;
using Luvi.Service.Navigation;

namespace UTCClock.Business.ViewModels
{
    public abstract class DigitalClockViewModelBase : ObservableObject, IObserver, INavigationAware
    {
        #region Properties

        private readonly ClockModel clock;
        private int hour;
        private int minute;
        private int second;
        protected TimeSpan timeZoneOffset;

        public int Hour
        {
            get { return this.hour; }
            protected set { base.Set<int>(ref this.hour, value); }
        }

        public int Minute
        {
            get { return this.minute; }
            protected set { base.Set<int>(ref this.minute, value); }
        }

        public int Second
        {
            get { return this.second; }
            protected set { base.Set<int>(ref this.second, value); }
        }

        public string TimeZone
        {
            get { return String.Format("{0}{1}", this.timeZoneOffset.Hours > -1 ? '+' : '-', this.timeZoneOffset.ToString(@"hh\:mm")); }
        }

        #endregion

        #region Constructors

        public DigitalClockViewModelBase()
        {
            this.clock = ClockModel.Instance;
            this.clock.Subscribe(this);
        }

        #endregion

        #region IObserver
        public void Update()
        {
            DateTimeOffset dateTime = new DateTimeOffset(this.clock.Time).ToOffset(this.timeZoneOffset);

            this.Hour = dateTime.Hour;
            this.Minute = dateTime.Minute;
            this.Second = dateTime.Second;
        }

        #endregion

        #region INavigationAware

        public void OnNavigatedTo(object argument, NavigationType navigationMode)
        {
            if (!(argument is TimeSpan))
            {
                throw new ArgumentOutOfRangeException("argument", "argument has to be of type TimeSpan");
            }

            this.timeZoneOffset = (TimeSpan)argument;
            RaisePropertyChanged("TimeZone");
        }

        public void OnNavigatedFrom() { }

        #endregion
    }
}
