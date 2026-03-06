using _Project.Application.States.GameState;

namespace _Project.Application.Interfaces
{
    public interface IGameState
    {
        GameStateType Type { get; }
        void Enter();
        void Exit();
    }
}