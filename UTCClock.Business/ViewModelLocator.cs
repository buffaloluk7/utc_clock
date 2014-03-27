using System;
using UTCClock.Business.ViewModels;
using ViHo.Service.Navigation;

namespace UTCClock.Business
{
    public sealed class ViewModelLocator
    {
        #region Properties

        public static INavigationWPFService NavigationService
        {
            get;
            private set;
        }

        #endregion

        #region Register Method

        public void Register(INavigationWPFService navigationService)
        {
            ViewModelLocator.NavigationService = navigationService;
        }

        #endregion

        #region ViewModels

        public MainWindowViewModel MainWindow
        {
            get { return new MainWindowViewModel(); }
        }

        public BeigeClockWindowViewModel BeigeClockWindow
        {
            get { return new BeigeClockWindowViewModel(); }
        }

        public BlueClockWindowViewModel BlueClockWindow
        {
            get { return new BlueClockWindowViewModel(); }
        }

        public CoralClockWindowViewModel CoralClockWindow
        {
            get { return new CoralClockWindowViewModel(); }
        }

        public GreyClockWindowViewModel GreyClockWindow
        {
            get { return new GreyClockWindowViewModel(); }
        }

        #endregion
    }
}
