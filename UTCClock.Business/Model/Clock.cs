using GalaSoft.MvvmLight;
using System;

namespace UTCClock.Business.Model
{
    internal sealed class Clock : ObservableObject
    {
        #region Properties

        #region Singleton

        private readonly static Clock instance = new Clock(DateTime.Now);

        public static Clock Instance
        {
            get { return Clock.instance; }
        }

        #endregion

        private DateTime time;

        public DateTime Time
        {
            get { return this.time; }
            set { base.Set<DateTime>(ref this.time, value); }
        }

        #endregion

        #region Constructors

        static Clock() { }

        public Clock(DateTime time)
        {
            this.time = time;
        }

        #endregion

    }
}
