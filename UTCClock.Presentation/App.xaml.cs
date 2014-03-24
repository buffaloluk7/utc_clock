using System;
using System.Collections.Generic;
using System.Windows;
using UTCClock.Business;
using UTCClock.Business.ViewModels;
using UTCClock.Presentation.Views;
using ViHo.WPF.Service.Navigation;

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
            viewViewModelMapper.Add(typeof(DigitalClock1WindowViewModel), typeof(DigitalClock1Window));
            viewViewModelMapper.Add(typeof(DigitalClock2WindowViewModel), typeof(DigitalClock2Window));

            var locator = App.Current.Resources["Locator"] as ViewModelLocator;
            var navigationService = new NavigationService(viewViewModelMapper);

            locator.Register(navigationService);

            this.Navigating -= App_Navigating;
        }
    }
}
