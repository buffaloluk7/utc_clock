using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;
using UTCClock.Business.ViewModels;

namespace UTCClock.Business.Commands
{
    class ShowCommand : ICommand
    {
        #region Properties

        private double x;
        private double y;
        private ClockType? clockType;
        private string timezone;

        #endregion

        #region Constructors

        public ShowCommand(ClockType clockType, string timezone, double x, double y)
        {
            this.x = x;
            this.y = y;
            this.clockType = clockType;
            this.timezone = timezone;
        }

        #endregion

        #region ICommand Implementations

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            switch (this.clockType)
            {
                case ClockType.Beige:
                    this.navigate<DigitalClock1WindowViewModel>();
                    break;

                case ClockType.Blue:
                    this.navigate<DigitalClock2WindowViewModel>();
                    break;

                case ClockType.Coral:
                    this.navigate<DigitalClock3WindowViewModel>();
                    break;

                case ClockType.Grey:
                    this.navigate<DigitalClock4WindowViewModel>();
                    break;

                // ClockType not set - ClockType.NONE
                default:
                    this.navigate<DigitalClock1WindowViewModel>();
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void navigate<T>()
        {
            if (x < 0 && y < 0)
            {
                ViewModelLocator.NavigationService.Navigate<T>();
            }
            else
            {
                ViewModelLocator.NavigationService.Navigate<T>();
            }
        }

        #endregion
    }
}
