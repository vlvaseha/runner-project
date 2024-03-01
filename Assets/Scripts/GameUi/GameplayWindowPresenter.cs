using Gameplay;
using UnityEngine.Events;

namespace GameUi
{
    public class GameplayWindowPresenter : BaseWindowPresenter<GameplayWindow>
    {
        #region Events

        public UnityEvent StartButtonClicked { get; }
        
        #endregion
        
        #region Class lifecycle

        public GameplayWindowPresenter(WindowType type) : base(type)
        {
            StartButtonClicked = new UnityEvent();
        }
        
        #endregion

        #region Methods

        public override void Initialize(GameplayWindow windowView)
        {
            base.Initialize(windowView);
        }

        public void StartButtonPressed()
        {
            StartButtonClicked?.Invoke();
        }

        public IPlayerInput GetPlayerInput() => WindowView.CreateUiPlayerInput();

        #endregion
        
        
    }
}
