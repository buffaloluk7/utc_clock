using System.Collections.Generic;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Common
{
    public abstract class ObservableSubjectBase : IObservable
    {
        private List<IObserver> observers = new List<IObserver>();

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
            // Eventuell in Tasks auslagern
            foreach(IObserver observer in this.observers)
            {
                if (observer != null)
                {
                    observer.Update();
                }
            }
            this.observers.RemoveAll(o => o == null);
        }
    }
}
