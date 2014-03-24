using System.Collections.Generic;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business.Common
{
    public abstract class ObservableSubjectBase : IObservable
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            // Eventuell in Tasks auslagern
            foreach(IObserver observer in observers)
            {
                System.Diagnostics.Debug.WriteLine("Notifying " + observer.ToString());
                observer.Update();
            }
        }
    }
}
