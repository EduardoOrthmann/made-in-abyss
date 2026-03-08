using UnityEngine;
using UnityEngine.Events;
using System;

namespace _Project.Application.Events
{
    [CreateAssetMenu(menuName = "Project/Events/Transition Event Channel", fileName = "TransitionEventChannel")]
    public class TransitionEventChannel : ScriptableObject
    {
        public event UnityAction<bool, float, Action> OnTransitionRequested;

        /// <param name="fadeToBlack">True to fade out to a black screen, False to fade in to the game.</param>
        /// <param name="duration">Duration of the fade in seconds.</param>
        /// <param name="onComplete">Callback executed when the fade finishes.</param>
        public void RaiseEvent(bool fadeToBlack, float duration, Action onComplete = null)
        {
            OnTransitionRequested?.Invoke(fadeToBlack, duration, onComplete);
        }
    }
}