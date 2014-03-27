using System;
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
        private ClockType clockType;
        private TimeSpan timeZone;

        #endregion

        #region Constructors

        public ShowCommand(ClockType clockType, TimeSpan timeZone, double x, double y)
        {
            this.clockType = clockType;
            this.timeZone = timeZone;
            this.x = x;
            this.y = y;
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
                    this.navigate<BeigeClockWindowViewModel>();
                    break;

                case ClockType.Blue:
                    this.navigate<BlueClockWindowViewModel>();
                    break;

                case ClockType.Coral:
                    this.navigate<CoralClockWindowViewModel>();
                    break;

                case ClockType.Grey:
                    this.navigate<GreyClockWindowViewModel>();
                    break;

                // ClockType not set - ClockType.NONE
                default:
                    this.navigate<BeigeClockWindowViewModel>();
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void navigate<T>()
        {
            if (x < 0 && y < 0)
            {
                ViewModelLocator.NavigationService.Navigate<T>(this.timeZone);
            }
            else
            {
                ViewModelLocator.NavigationService.Navigate<T>(this.timeZone, null, x, y);
            }
        }

        #endregion
    }
}
