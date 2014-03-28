using System;
using ViHo.Service.Navigation;
using ViHo.Json.Extension;

namespace UTCClock.Business.ViewModels
{
    public class CoralClockWindowViewModel : DigitalClockViewModelBase, INavigationAware
    {
        public CoralClockWindowViewModel() : base() { }

        #region INavigationAware Implementations

        public async void OnNavigatedTo(string jsonString, NavigationType navigationMode)
        {
            this.timeZoneOffset = await jsonString.FromJson<TimeSpan>();
        }

        public void OnNavigatedFrom() { }

        #endregion
    }
}
