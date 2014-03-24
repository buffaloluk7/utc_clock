using UTCClock.Business.ViewModels;
using ViHo.Service.Navigation;

namespace UTCClock.Business
{
    public sealed class ViewModelLocator
    {
        private INavigationService navigationService;

        public void Register(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public MainWindowViewModel MainWindow
        {
            // Könnte man mit Ninject schöner gestalten
            get { return new MainWindowViewModel(navigationService); }
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
            get { return new DigitalClock3WindowViewModel(); }
        }

        public DigitalClock4WindowViewModel DigitalClock4Window
        {
            get { return new DigitalClock4WindowViewModel(); }
        }
    }
}
