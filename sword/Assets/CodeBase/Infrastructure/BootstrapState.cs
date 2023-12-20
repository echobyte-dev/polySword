using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
      RegisterServices();
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadLevelState, string>("Level1");

    public void Exit()
    {
    }

    private static void RegisterServices()
    {
      Game.InputService = RegisterInputService();
    }

    private static IInputService RegisterInputService() =>
      new StandaloneInputService();
  }
}