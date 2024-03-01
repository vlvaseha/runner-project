using UnityEngine.Events;

namespace GameUi
{
    public class GameplayWindowPresenter : BaseWindowPresenter<GameplayWindow>
    {
        #region Events

        public UnityEvent OnPointerDown { get; }

        #endregion
        
        #region Class lifecycle

        public GameplayWindowPresenter(WindowType type) : base(type)
        {
            OnPointerDown = new UnityEvent();
        }
        
        #endregion

        #region Methods

        public void ProcessUiPointerDown()
        {
            OnPointerDown?.Invoke();
        }

        #endregion
        
        
    }
}
