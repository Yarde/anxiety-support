using System.Collections.Generic;

namespace Yarde.Utils.CommandReceiver
{
    public class StackCommandInvoker<TCommand> : ICommandInvoker<TCommand> where TCommand : ICommand
    {
        private readonly Stack<TCommand> _commandStack = new();

        public void AddCommand(TCommand command)
        {
            command.Execute();
            _commandStack.Push(command);
        }

        public void UndoCommand()
        {
            if (_commandStack.Count > 0)
            {
                _commandStack.Pop().Undo();
            }
        }
    }
}