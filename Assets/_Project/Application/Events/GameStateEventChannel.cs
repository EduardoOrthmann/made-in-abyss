using UnityEngine;
using UnityEngine.Events;
using System;

namespace _Project.Application.Events
{
    [CreateAssetMenu(menuName = "Project/Events/Game State Event Channel", fileName = "GameStateEventChannel")]
    public class GameStateEventChannel : ScriptableObject
    {
        public event UnityAction<Type> OnStateChanged;

        public void RaiseEvent(Type stateType)
        {
            OnStateChanged?.Invoke(stateType);
        }
    }
}