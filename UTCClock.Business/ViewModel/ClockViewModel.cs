using System;
using GalaSoft.MvvmLight;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModel
{
    public class ClockViewModel : ViewModelBase
    {
        #region Properties

        public DateTime Time
        {
            get { return Clock.Instance.Time; }
        }

        #endregion

        #region Constructors

        #endregion

        #region Commands

        #endregion
    }
}
