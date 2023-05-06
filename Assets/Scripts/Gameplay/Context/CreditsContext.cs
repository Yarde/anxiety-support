using UnityEngine;
using UnityEngine.Video;
using VContainer;
using VContainer.Unity;
using Yarde.Gameplay.Scenes;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay.Context
{
    public class CreditsContext : SceneContext
    {

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CreditsScene>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<CanvasManager>();
            builder.Register<QuestSystem>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<CreditsScreen>();
        }
    }
}