namespace UTCClock.Business.Interfaces
{
    public interface IObservable
    {
        void Subscribe(IObserver observer);

        void Unsubscribe(IObserver observer);

        void Notify();
    }
}
