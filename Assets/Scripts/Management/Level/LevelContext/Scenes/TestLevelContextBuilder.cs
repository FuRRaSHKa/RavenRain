using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Management.Factories;

namespace HalloGames.RavensRain.Management.Level
{
    public class TestLevelContextBuilder : ContextBuilder
    {
        protected override void BuildServices()
        {
            base.BuildServices();

            BulletFactory bulletFactory = new BulletFactory(ServiceProvider);
            ServiceProvider.AddService<IProjectileFactory>(bulletFactory);
        }
    }
}