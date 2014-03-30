﻿using System;
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
            base.timeZoneOffset = await jsonString.FromJson<TimeSpan>();
            RaisePropertyChanged("TimeZone");
        }

        public void OnNavigatedFrom() { }

        #endregion
    }
}
