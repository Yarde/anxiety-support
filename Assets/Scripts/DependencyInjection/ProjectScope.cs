using VContainer;
using VContainer.Unity;
using Yarde.Gameplay;

namespace Yarde.DependencyInjection
{
    public class ProjectScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            //builder.Register<GameplayBoot>(Lifetime.Scoped);
        }
    }
}