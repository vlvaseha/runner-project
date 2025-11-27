using Gameplay;
using UnityEngine;

namespace GameUi
{
    public class GameplayWindow : Window
    {
        [SerializeField] private UiPlayerInput inputPanel;
        
        public IPlayerInput GetPlayerInput() => inputPanel;

        protected override void OnOpenStarted()
        {
            GameplayWindowPresenter gameplayWindowPresenter = (GameplayWindowPresenter) Arguments;
            gameplayWindowPresenter.SetView(this);
        }
    }
}

