using UTCClock.Business.Interfaces;

namespace UTCClock.Business.ViewModels
{
    public class DigitalClock1WindowViewModel : ClockViewModelBase, IObserver
    {
        #region Constructors

        public DigitalClock1WindowViewModel() : base()
        {
            base.clock.Subscribe(this);
        }

        #endregion

        #region Implementations

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("DigitalClock received update!");

            base.Hour = this.clock.Time.Hour;
            base.Minute = this.clock.Time.Minute;
            base.Second = this.clock.Time.Second;
        }

        #endregion
    }
}
