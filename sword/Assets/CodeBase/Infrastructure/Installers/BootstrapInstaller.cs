using CodeBase.Infrastructure.Factories;
using CodeBase.Player;
using CodeBase.Services;
using CodeBase.Services.Input;
using Zenject;

namespace CodeBase.Infrastructure.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }
  }
}