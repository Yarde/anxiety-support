using JetBrains.Annotations;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay.Scenes
{
    [UsedImplicitly]
    public class CreditsScene : BaseScene
    {
        public CreditsScene(GameplayFlow gameplayFlow, QuestSystem questSystem, CanvasManager canvasManager)
            : base(gameplayFlow, questSystem, canvasManager)
        {
        }

        protected override void InternalStart()
        {
        }
    }
}