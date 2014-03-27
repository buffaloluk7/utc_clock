using System;
using ViHo.Json.Extension;
using ViHo.Service.Navigation;

namespace UTCClock.Business.ViewModels
{
    public class GreyClockWindowViewModel : DigitalClockViewModelBase, INavigationAware
    {
        public GreyClockWindowViewModel() : base() { }

        #region INavigationAware Implementations

        public async void OnNavigatedTo(string jsonString, NavigationType navigationMode)
        {
            this.timeZoneOffset = await jsonString.FromJson<TimeSpan>();
        }

        public void OnNavigatedFrom() { }

        #endregion
    }
}
