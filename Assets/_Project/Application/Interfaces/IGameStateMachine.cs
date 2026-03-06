using _Project.Application.States.GameState;

namespace _Project.Application.Interfaces
{
    public interface IGameStateMachine
    {
        GameStateType CurrentStateType { get; }

        void ChangeState(GameStateType newStateType);
    }
}