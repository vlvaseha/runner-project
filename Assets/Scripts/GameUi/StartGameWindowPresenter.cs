using System;

namespace GameUi
{
    public class StartGameWindowPresenter : BaseWindowPresenter<StartGameWindow>
    {
        public event Action OnStartButtonClicked;

        public void StartButtonPressed() => OnStartButtonClicked?.Invoke();
    }
}
