﻿using System;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Model
{
    public sealed class Clock : ObservableSubjectBase
    {
        #region Properties

        #region Singleton

        private readonly static Clock instance = new Clock(DateTime.Now);

        public static Clock Instance
        {
            get { return Clock.instance; }
        }

        #endregion

        private DateTime time;

        public DateTime Time
        {
            get { return this.time; }
            set 
            { 
                this.time = value;
                this.Notify();
                System.Diagnostics.Debug.WriteLine("Setting time to " + time.ToString());
            }
        }

        #endregion

        #region Constructors

        static Clock() { }

        public Clock(DateTime time)
        {
            this.time = time;
        }

        #endregion
    }
}
