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

            _inputProvider.OnPauseAction += HandlePauseAction;
        }

        private void Start()
        {
            _gameStateMachine.ChangeState(GameStateType.MainMenu);
            _transitionEventChannel.RaiseEvent(false, 0f);
        }

        public void StartGame()
        {
            _transitionEventChannel.RaiseEvent(true, 0.5f, () =>
            {
                _gameStateMachine.ChangeState(GameStateType.Playing);
                _transitionEventChannel.RaiseEvent(false, 0.5f);
            });
        }

        public void ReturnToMenu()
        {
            _transitionEventChannel.RaiseEvent(true, 0.5f, () =>
            {
                _gameStateMachine.ChangeState(GameStateType.MainMenu);
                _transitionEventChannel.RaiseEvent(false, 0.5f);
            });
        }

        public void PauseGame() => _gameStateMachine.ChangeState(GameStateType.Paused);

        public void ResumeGame() => _gameStateMachine.ChangeState(GameStateType.Playing);

        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        private void HandlePauseAction()
        {
            if (_gameStateMachine.CurrentStateType == GameStateType.Playing)
            {
                PauseGame();
            }
            else if (_gameStateMachine.CurrentStateType == GameStateType.Paused)
            {
                ResumeGame();
            }
        }

        public void Dispose()
        {
            if (_inputProvider == null) return;

            _inputProvider.OnPauseAction -= HandlePauseAction;
        }
    }
}