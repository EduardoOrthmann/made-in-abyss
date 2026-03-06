using System.Collections.Generic;
using System.Linq;
using _Project.Application.Interfaces;


namespace _Project.Application.States.GameState
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<GameStateType, IGameState> _states;
        private IGameState _currentState;

        public GameStateType CurrentStateType { get; private set; }

        public GameStateMachine(List<IGameState> states)
        {
            _states = states.ToDictionary(s => s.Type, s => s);
        }

        public void ChangeState(GameStateType newStateType)
        {
            if (!_states.TryGetValue(newStateType, out var nextState)) return;

            _currentState?.Exit();
            CurrentStateType = newStateType;
            _currentState = nextState;
            _currentState?.Enter();
        }
    }
}