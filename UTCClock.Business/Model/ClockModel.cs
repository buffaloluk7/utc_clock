using System;
using UTCClock.Business.Common;

namespace UTCClock.Business.Model
{
    public sealed class ClockModel : ObservableSubjectBase
    {
        #region Properties

        private DateTime time;

        public DateTime Time
        {
            get { return this.time; }
            set 
            { 
                this.time = value;
                this.Notify();
                System.Diagnostics.Debug.WriteLine("Time set to " + Time.ToString());
            }
        }

        #endregion

        #region Singleton

        private static volatile ClockModel instance;
        private static object syncRoot = new Object();

        public static ClockModel Instance
        {
            get
            {
                if (ClockModel.instance == null)
                {
                    lock (ClockModel.syncRoot)
                    {
                        if (ClockModel.instance == null)
                        {
                            ClockModel.instance = new ClockModel(DateTime.Now);
                        }
                    }
                }

                return ClockModel.instance;
            }
        }

        #endregion

        #region Constructors

        private ClockModel(DateTime time)
        {
            this.Time = time;
        }

        #endregion
    }
}
