using UnityEngine;
using UnityEngine.Video;
using VContainer;
using VContainer.Unity;
using Yarde.Gameplay.Scenes;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay.Context
{
    public class VideoContext : SceneContext
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<VideoScene>(Lifetime.Scoped);
            builder.Register<QuestSystem>(Lifetime.Scoped);
            
            builder.RegisterComponentInHierarchy<CanvasManager>();

            builder.RegisterComponent(_videoPlayer);
        }
    }
}