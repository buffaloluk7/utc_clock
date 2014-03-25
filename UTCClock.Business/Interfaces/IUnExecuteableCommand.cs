namespace UTCClock.Business.Interfaces
{
    public interface IUnExecuteableCommand
    {
        void UnExecute(object argument = null);
    }
}
