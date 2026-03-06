using _Project.Application.Interfaces;
using _Project.Application.Events;

namespace _Project.Application.States.GameState
{
    public class MainMenuState : IGameState
    {
        public GameStateType Type => GameStateType.MainMenu;
        private readonly GameStateEventChannel _eventChannel;

        public MainMenuState(GameStateEventChannel eventChannel)
        {
            _eventChannel = eventChannel;
        }

        public void Enter() => _eventChannel.RaiseEvent(GameStateType.MainMenu);

        public void Exit() { }
    }
}