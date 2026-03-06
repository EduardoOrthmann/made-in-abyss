using UnityEngine;
using UnityEngine.UI;
using _Project.Presentation.Scripts.Controllers;

namespace _Project.Presentation.Scripts.Views
{
    public class MainMenuView : BaseStateView
    {
        [SerializeField] private Button startButton;
        [SerializeField] private GameController gameController;

        protected override void OnEnable()
        {
            base.OnEnable();
            startButton.onClick.AddListener(gameController.StartGame);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            startButton.onClick.RemoveListener(gameController.StartGame);
        }
    }
}