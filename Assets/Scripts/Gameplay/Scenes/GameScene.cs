using System;

namespace Yarde.Gameplay.Scenes
{
    public interface IScene
    {
        void Start(string questId, Action onSuccess, Action onFail);
    }
}