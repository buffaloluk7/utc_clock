using System;
using UTCClock.Business.Common;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModels
{
    public abstract class DigitalClockViewModelBase : ObservableObject, IObserver
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

        #endregion

        #region Constructors

        public DigitalClockViewModelBase()
        {
            this.clock = ClockModel.Instance;
            this.clock.Subscribe(this);
        }

        #endregion

        #region IObserver Implementations
        public void Update()
        {
            DateTimeOffset dateTime = new DateTimeOffset(this.clock.Time).ToOffset(this.timeZoneOffset);

            this.Hour = dateTime.Hour;
            this.Minute = dateTime.Minute;
            this.Second = dateTime.Second;
        }

        #endregion
    }
}
