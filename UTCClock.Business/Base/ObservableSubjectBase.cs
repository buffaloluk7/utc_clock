using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTCClock.Business.Interfaces;

namespace UTCClock.Business
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
            foreach(IObserver observer in observers)
            {
                System.Diagnostics.Debug.WriteLine("Notifying " + observer.ToString());
                observer.Update(this);
            }
        }
    }
}
