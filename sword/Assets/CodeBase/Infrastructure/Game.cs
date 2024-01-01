using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IGameFactory gameFactory,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IStaticDataService staticData, IUIFactory uiFactory)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, gameFactory, progressService, saveLoadService, staticData, uiFactory);
        }

    }
}