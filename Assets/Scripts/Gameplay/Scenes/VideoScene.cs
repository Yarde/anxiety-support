using JetBrains.Annotations;
using VContainer;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay.Scenes
{
    [UsedImplicitly]
    public class VideoScene : BaseScene
    {
        public VideoScene(GameplayFlow gameplayFlow, QuestSystem questSystem, CanvasManager canvasManager) 
            : base(gameplayFlow, questSystem, canvasManager)
        {
        }

        protected override void InternalStart()
        {
        }
    }
}