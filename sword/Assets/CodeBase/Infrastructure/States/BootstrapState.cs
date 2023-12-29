using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private IInputService _inputService;
    private IStaticDataService _staticData;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IStaticDataService staticData)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _staticData = staticData;
    }

    public void Enter()
    {
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
      RegisterService();
    }

    public void Exit()
    {
    }

    private void RegisterService()
    {
      _staticData.Load();
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadProgressState>();
  }
}