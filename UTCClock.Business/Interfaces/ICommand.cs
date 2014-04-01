namespace UTCClock.Business.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        string Description { get; }

        ICommand Make(string arguments);

        bool CanExecute(string arguments);

        void Execute();
    }
}
