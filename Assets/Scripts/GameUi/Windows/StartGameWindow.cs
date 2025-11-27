using UnityEngine;

namespace GameUi
{
    public class StartGameWindow : Window
    {
        [SerializeField] private UiStartPanel startPanel;

        private StartGameWindowPresenter _startGameWindowPresenter;

        protected override void OnOpenStarted()
        {
            _startGameWindowPresenter = (StartGameWindowPresenter) Arguments;
            _startGameWindowPresenter.SetView(this);

            startPanel.OnStartGameButtonClicked += StartButtonClickedHandler;
        }

        protected override void OnCloseStarted()
        {
            startPanel.OnStartGameButtonClicked -= StartButtonClickedHandler;
        }

        private void StartButtonClickedHandler()
        {
            _startGameWindowPresenter.StartButtonPressed();
        }
    }
}
