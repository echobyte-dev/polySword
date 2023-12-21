using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public static IInputService InputService;
    
    public LoadingCurtain CurtainPrefab;
    
    private Game _game;
    private IGameFactory _gameFactory;
    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;

    [Inject]
    public void Construct(IGameFactory gameFactory, IInputService inputService, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      InputService = inputService;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }

    private void Awake()
    {
      _game = new Game(this, Instantiate(CurtainPrefab), _gameFactory, _progressService, _saveLoadService);
      _game.StateMachine.Enter<BootstrapState>();
      
      DontDestroyOnLoad(this);
    }
  }
}