namespace UTCClock.Business.Interfaces
{
    public interface ICommand
    {
        bool canExecute();

        void Execute();
    }
}
