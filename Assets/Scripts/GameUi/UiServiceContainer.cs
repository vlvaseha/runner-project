namespace GameUi
{
    public class UiServiceContainer 
    {
        #region Properties

        public static UiServiceContainer Instance { get; private set; }

        #endregion

        #region Window presenters

        public GameplayWindowPresenter GameplayWindowPresenter { get; }

        #endregion

        #region Class lifecycle

        public UiServiceContainer(GameplayWindowPresenter gameplayWindowPresenter)
        {
            Instance = this;

            GameplayWindowPresenter = gameplayWindowPresenter;
        }
        
        #endregion
    }
}