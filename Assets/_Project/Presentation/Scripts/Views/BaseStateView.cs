using _Project.Application.Events;
using _Project.Application.States.GameState;
using UnityEngine;

namespace _Project.Presentation.Scripts.Views
{
    public abstract class BaseStateView : MonoBehaviour
    {
        [SerializeField] private GameStateEventChannel eventChannel;
        [SerializeField] private GameStateType targetState;
        [SerializeField] private GameObject visualPanel;

        protected virtual void OnEnable()
        {
            eventChannel.OnStateChanged += HandleStateChanged;
        }

        protected virtual void OnDisable()
        {
            eventChannel.OnStateChanged -= HandleStateChanged;
        }

        private void HandleStateChanged(GameStateType state)
        {
            visualPanel.SetActive(state == targetState);
        }
    }
}