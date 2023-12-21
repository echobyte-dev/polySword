using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;

    private void Awake()
    {
      GameBootstrapper bootStrapper = FindObjectOfType<GameBootstrapper>();
      if (bootStrapper != null)
        return;

      Instantiate(_gameBootstrapperPrefab);
    }
  }
}