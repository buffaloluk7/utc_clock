namespace UTCClock.Business.Interfaces
{
    public interface ICommand
    {
        bool canExecute(object argument = null);

        void Execute(object argument = null);
    }
}
