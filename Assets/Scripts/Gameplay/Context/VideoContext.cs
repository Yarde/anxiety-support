using UnityEngine;
using UnityEngine.Video;
using VContainer;
using VContainer.Unity;
using Yarde.Gameplay.Scenes;
using Yarde.Quests;

namespace Yarde.Gameplay.Context
{
    public class VideoContext : SceneContext
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IScene, VideoScene>(Lifetime.Scoped);
            builder.Register<QuestSystem>(Lifetime.Scoped);

            builder.RegisterComponent(_videoPlayer);
        }
    }
}