using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain _curtain;
    private Game _game;
    private IGameFactory _gameFactory;
    public static IInputService InputService; 
    
    [Inject]
    public void Construct(IGameFactory gameFactory, IInputService inputService)
    {
      InputService = inputService;
      _gameFactory = gameFactory;
    }

    private void Awake()
    {
      _game = new Game(this, _curtain, _gameFactory);
      _game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}