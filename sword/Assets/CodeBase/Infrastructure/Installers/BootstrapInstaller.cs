using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
      
      Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
      Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
      
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      
      Container.Bind<IAssets>().To<AssetProvider>().AsSingle();
      Container.Bind<IRandomService>().To<RandomService>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
      
      Container.Bind<IWindowService>().To<WindowService>().AsSingle();
      Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
    }
  }
}