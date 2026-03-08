using UnityEngine;
using Zenject;
using System;
using _Project.Application.Events;
using _Project.Application.Interfaces;
using _Project.Application.States.GameState;

namespace _Project.Presentation.Scripts.Controllers
{
    public class GameController : MonoBehaviour, IDisposable
    {
        private IGameStateMachine _gameStateMachine;
        private IInputProvider _inputProvider;
        private TransitionEventChannel _transitionEventChannel;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IInputProvider inputProvider, TransitionEventChannel transitionEventChannel)
        {
            _gameStateMachine = stateMachine;
            _inputProvider = inputProvider;
            _transitionEventChannel = transitionEventChannel;

            _inputProvider.OnPauseAction += TogglePause;
        }

        private void Start()
        {
            _gameStateMachine.ChangeState<MainMenuState>();
            _transitionEventChannel.RaiseEvent(false, 0f);
        }

        public void RequestStateChange<TState>(bool useTransition = true) where TState : class, IGameState
        {
            if (useTransition)
            {
                _transitionEventChannel.RaiseEvent(true, 0.5f, () =>
                {
                    _gameStateMachine.ChangeState<TState>();
                    _transitionEventChannel.RaiseEvent(false, 0.5f, null);
                });
            }
            else
            {
                _gameStateMachine.ChangeState<TState>();
            }
        }

        public void StartGameFromMenu() => RequestStateChange<PlayingState>();

        public void ResumeGame() => RequestStateChange<PlayingState>(false);

        public void PauseGame() => RequestStateChange<PausedState>(false);

        public void RequestMainMenuState() => RequestStateChange<MainMenuState>();

        private void TogglePause()
        {
            if (_gameStateMachine.CurrentStateType == typeof(PlayingState)) PauseGame();

            else if (_gameStateMachine.CurrentStateType == typeof(PausedState)) ResumeGame();
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void Dispose()
        {
            if (_inputProvider == null) return;

            _inputProvider.OnPauseAction -= TogglePause;
        }
    }
}