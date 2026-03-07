using UnityEngine;
using Zenject;
using System;
using _Project.Application.Interfaces;
using _Project.Application.States.GameState;

namespace _Project.Presentation.Scripts.Controllers
{
    public class GameController : MonoBehaviour, IDisposable
    {
        private IGameStateMachine _gameStateMachine;
        private IInputProvider _inputProvider;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IInputProvider inputProvider)
        {
            _gameStateMachine = stateMachine;
            _inputProvider = inputProvider;

            _inputProvider.OnPauseAction += HandlePauseAction;
        }

        private void Start()
        {
            _gameStateMachine.ChangeState(GameStateType.MainMenu);
        }

        public void StartGame() => _gameStateMachine.ChangeState(GameStateType.Playing);
        public void PauseGame() => _gameStateMachine.ChangeState(GameStateType.Paused);
        public void ResumeGame() => _gameStateMachine.ChangeState(GameStateType.Playing);
        public void ReturnToMenu() => _gameStateMachine.ChangeState(GameStateType.MainMenu);

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