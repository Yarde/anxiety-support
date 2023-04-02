using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.Gameplay;
using Yarde.Quests;
using Yarde.Scene;

namespace Yarde.DependencyInjection
{
    public class ProjectContext : LifetimeScope
    {
        [SerializeField] private Questline _questline;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneController>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayFlow>();
            builder.RegisterInstance(_questline);
        }
    }
}