using System;
using UTCClock.Business.ViewModels;
using ViHo.Service.Navigation;

namespace UTCClock.Business
{
    public sealed class ViewModelLocator
    {
        #region Properties

        public static INavigationService NavigationService
        {
            get;
            private set;
        }

        #endregion

        #region Register Method

        public void Register(INavigationService navigationService)
        {
            ViewModelLocator.NavigationService = navigationService;
        }

        #endregion

        #region ViewModels

        public MainWindowViewModel MainWindow
        {
            get { return new MainWindowViewModel(); }
        }

        public DigitalClock1WindowViewModel DigitalClock1Window
        {
            get { return new DigitalClock1WindowViewModel(); }
        }

        public DigitalClock2WindowViewModel DigitalClock2Window
        {
            get { return new DigitalClock2WindowViewModel(); }
        }

        public DigitalClock3WindowViewModel DigitalClock3Window
        {
            get { return new DigitalClock3WindowViewModel(new TimeSpan(3,0,0)); }
        }

        public DigitalClock4WindowViewModel DigitalClock4Window
        {
            get { return new DigitalClock4WindowViewModel(); }
        }

        #endregion
    }
}
