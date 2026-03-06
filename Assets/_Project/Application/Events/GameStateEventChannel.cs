using UnityEngine;
using UnityEngine.Events;
using _Project.Application.States.GameState;

namespace _Project.Application.Events
{
    [CreateAssetMenu(menuName = "Project/Events/Game State Event Channel", fileName = "GameStateEventChannel")]
    public class GameStateEventChannel : ScriptableObject
    {
        public event UnityAction<GameStateType> OnStateChanged;

        public void RaiseEvent(GameStateType newState)
        {
            OnStateChanged?.Invoke(newState);
        }
    }
}