namespace UTCClock.Business.Interfaces
{
    public interface IUndoableCommand : ICommand
    {
        void UnExecute();
    }
}
