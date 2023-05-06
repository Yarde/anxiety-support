using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.Audio;
using Yarde.Gameplay;
using Yarde.Quests;
using Yarde.Scene;

namespace Yarde.DependencyInjection
{
    public class ProjectContext : LifetimeScope
    {
        [SerializeField] private Questline _questline;

        protected override void Awake()
        {
            base.Awake();
            Container.Resolve<GameplayFlow>().Start();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneController>(Lifetime.Singleton);
            builder.Register<GameplayFlow>(Lifetime.Singleton);
            builder.RegisterInstance(_questline);
            builder.RegisterComponentInHierarchy<AudioManager>();
        }
    }
}