using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    
    [Inject]
    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public GameObject CreatePlayer(GameObject at) =>
      _assets.Instantiate(AssetPath.PlayerPath, at: at.transform.position);
  }
}