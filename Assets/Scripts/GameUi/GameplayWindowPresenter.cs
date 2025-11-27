using Gameplay;

namespace GameUi
{
    public class GameplayWindowPresenter : BaseWindowPresenter<GameplayWindow>
    {
        public bool TryGetInputPanel(out IPlayerInput playerInput)
        {
            playerInput = IsViewInitialized ? WindowView.GetPlayerInput() : null;
            return playerInput is not null;
        }
    }
}

