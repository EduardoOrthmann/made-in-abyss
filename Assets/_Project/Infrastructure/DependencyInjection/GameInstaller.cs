using _Project.Application.Commands;
using Zenject;
using UnityEngine;
using _Project.Application.Interfaces;
using _Project.Application.Events;
using _Project.Application.States.GameState;
using _Project.Infrastructure.Adapters;
using UnityEngine.InputSystem;

namespace _Project.Infrastructure.DependencyInjection
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameStateEventChannel gameStateEventChannel;
        [SerializeField] private TransitionEventChannel transitionEventChannel;
        [SerializeField] private InputActionReference pauseInputAction;

        public override void InstallBindings()
        {
            // Event Channels
            Container.BindInstance(gameStateEventChannel).AsSingle();
            Container.BindInstance(transitionEventChannel).AsSingle();

            // Services
            Container.Bind<ITimeService>().To<UnityTimeAdapter>().AsSingle();
            Container.Bind<ISceneLoader>().To<UnitySceneLoader>().AsSingle();

            // Commands
            Container.Bind<CommandProcessor>().AsSingle();
            Container.BindFactory<Domain.ScriptableObjects.LevelData, System.Action, LoadLevelCommand, LoadLevelCommand.Factory>().AsSingle();
            Container.BindFactory<Domain.ScriptableObjects.LevelData, System.Action, UnloadLevelCommand, UnloadLevelCommand.Factory>().AsSingle();

            // States
            Container.Bind<IGameState>().To<BootstrapState>().AsSingle();
            Container.Bind<IGameState>().To<MainMenuState>().AsSingle();
            Container.Bind<IGameState>().To<PlayingState>().AsSingle();
            Container.Bind<IGameState>().To<PausedState>().AsSingle();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.BindInstance(pauseInputAction).WhenInjectedInto<InputAdapter>();
            Container.BindInterfacesTo<InputAdapter>().AsSingle();
        }
    }
}