﻿using System;
using System.Collections.Generic;
using System.Windows;
using UTCClock.Business;
using UTCClock.Business.ViewModels;
using UTCClock.Presentation.Views;
using Luvi.WPF.Service.Navigation;

namespace UTCClock.Presentation
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App ()
	    {
            this.Navigating += App_Navigating;
	    }

        void App_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Dictionary<Type, Type> viewViewModelMapper = new Dictionary<Type, Type>();
            viewViewModelMapper.Add(typeof(MainWindowViewModel), typeof(MainWindow));
            viewViewModelMapper.Add(typeof(BeigeClockWindowViewModel), typeof(BeigeClockWindow));
            viewViewModelMapper.Add(typeof(BlueClockWindowViewModel), typeof(BlueClockWindow));
            viewViewModelMapper.Add(typeof(CoralClockWindowViewModel), typeof(CoralClockWindow));
            viewViewModelMapper.Add(typeof(GreyClockWindowViewModel), typeof(GreyClockWindow));

            var locator = App.Current.Resources["Locator"] as ViewModelLocator;
            var navigationService = new NavigationService(viewViewModelMapper);
            
            locator.Register(navigationService);

            this.Navigating -= App_Navigating;
        }
    }
}
