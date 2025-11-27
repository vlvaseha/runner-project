using UnityEngine;

namespace GameUi
{
    public class GameplayWindow : Window
    {
        [SerializeField] private UiStartPanel startPanel;

        private GameplayWindowPresenter _gameplayWindowPresenter;

        protected override void OnOpenStarted()
        {
            _gameplayWindowPresenter = (GameplayWindowPresenter) Arguments;
            _gameplayWindowPresenter.Initialize(this);

            startPanel.OnStartGameButtonClicked += StartButtonClickedHandler;
        }

        protected override void OnCloseStarted()
        {
            startPanel.OnStartGameButtonClicked -= StartButtonClickedHandler;
        }

        private void StartButtonClickedHandler()
        {
            _gameplayWindowPresenter.StartButtonPressed();
        }
    }
}
