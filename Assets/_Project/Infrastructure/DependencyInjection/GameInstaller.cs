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
            Container.BindInstance(gameStateEventChannel).AsSingle();
            Container.BindInstance(transitionEventChannel).AsSingle();

            Container.Bind<ITimeService>().To<UnityTimeAdapter>().AsSingle();

            Container.Bind<IGameState>().To<MainMenuState>().AsSingle();
            Container.Bind<IGameState>().To<PlayingState>().AsSingle();
            Container.Bind<IGameState>().To<PausedState>().AsSingle();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.BindInstance(pauseInputAction).WhenInjectedInto<InputAdapter>();
            Container.BindInterfacesTo<InputAdapter>().AsSingle();
        }
    }
}