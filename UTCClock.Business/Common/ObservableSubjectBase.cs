using System.Collections.Generic;
using System.Threading.Tasks;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Common
{
    public abstract class ObservableSubjectBase : IObservable
    {
        #region Properties

        private List<IObserver> observers = new List<IObserver>();

        #endregion

        #region IObservable Implementations

        public void Subscribe(IObserver observer)
        {
            if (!this.observers.Contains(observer))
            {
                this.observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer)
        {
            if (this.observers.Contains(observer))
            {
                this.observers.Remove(observer);
            }
        }

        public void Notify()
        {
            Parallel.ForEach<IObserver>(this.observers, (o) =>
            {
                if (o != null)
                {
                    o.Update();
                }
            });
        }

        #endregion
    }
}
