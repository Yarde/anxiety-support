namespace Yarde.Utils.CommandReceiver
{
    public interface ICommandInvoker<TCommand> where TCommand : ICommand
    {
        void AddCommand(TCommand command);
        void UndoCommand();
        
    }
}