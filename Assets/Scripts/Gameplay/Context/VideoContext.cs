using VContainer;
using Yarde.Gameplay.Entities;
using Yarde.Gameplay.Scenes;
using Yarde.Quests;

namespace Yarde.Gameplay.Context
{
    public class VideoContext : SceneContext
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IScene, VideoScene>(Lifetime.Scoped);
            builder.Register<EntityManager>(Lifetime.Scoped);
            builder.Register<QuestSystem>(Lifetime.Scoped);
        }
    }
}