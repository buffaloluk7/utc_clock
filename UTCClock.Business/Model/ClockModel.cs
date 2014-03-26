using System;
using System.Collections.ObjectModel;
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

            //System.Diagnostics.Debug.WriteLine(timeZone.GetUtcOffset(DateTime.Now));  // liefert 01:00:00 als TimeSpan
            //System.Diagnostics.Debug.WriteLine(DateTimeOffset.Now);                   // liefert 26.03.2014 22:06:16 +01:00 als DateTimeOffset
            //System.Diagnostics.Debug.WriteLine(DateTimeOffset.UtcNow);                // liefert 26.03.2014 21:07:24 +00:00 als DateTimeOffset
            //System.Diagnostics.Debug.WriteLine(TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time")); // liefert (UTC) Dublin, Edinburgh, Lissabon, London
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            System.Diagnostics.Debug.WriteLine(dto.Offset); // liefert 01:00:00
            dto = dto.ToOffset(new TimeSpan(-10, 0, 0));
            System.Diagnostics.Debug.WriteLine(dto.Offset); // liefert 02:00:00
            // -> das brauchen wir, nur ist es ja so gedacht, dass jedes geöffnete Window ein unterschiedliches Offset darstellen können soll.
            // also darf es nicht im ClockModel verändert werden

            ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
            foreach (var t in tz)
            {
                System.Diagnostics.Debug.WriteLine(t.Id + " | " + t.StandardName + " | " + t.DisplayName + " | " + t.BaseUtcOffset);
            }

        }

        #endregion
    }
}
