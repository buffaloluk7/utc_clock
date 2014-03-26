namespace UTCClock.Business.Interfaces
{
    public interface ICommand
    {
        bool CanExecute();

        void Execute();
    }
}
