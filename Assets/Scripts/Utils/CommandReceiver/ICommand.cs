using Cysharp.Threading.Tasks;

namespace Yarde.Utils.CommandReceiver
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}