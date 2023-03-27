using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.Gameplay;
using Yarde.Quests;

namespace Yarde.DependencyInjection
{
    public class ProjectContext : LifetimeScope
    {
        [SerializeField] private Questline _questline;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayFlow>(Lifetime.Scoped);
            builder.RegisterInstance(_questline);
        }
    }
}