namespace Yarde.Utils.CommandReceiver
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}