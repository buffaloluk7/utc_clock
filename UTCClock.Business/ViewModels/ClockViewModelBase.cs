using UTCClock.Business.Common;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModels
{
    public abstract class ClockViewModelBase : ObservableObject
    {
        #region Properties

        protected readonly ClockModel clock;
        protected int hour;
        protected int minute;
        protected int second;

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

        public ClockViewModelBase()
        {
            this.clock = ClockModel.Instance;
        }

        #endregion
    }
}
