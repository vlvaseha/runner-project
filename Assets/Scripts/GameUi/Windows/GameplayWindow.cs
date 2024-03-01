using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUi
{
    public class GameplayWindow : Window, IPointerDownHandler 
    {
        #region Fields

        [SerializeField] private TapToPlayView _tapToPlayView;
        
        private GameplayWindowPresenter _gameplayWindowPresenter;

        #endregion
        
        #region Methods

        public override void Show()
        {
            _gameplayWindowPresenter = Arguments as GameplayWindowPresenter;
            _gameplayWindowPresenter.SetView(this);
        }

        public override void Hide()
        {
            
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _gameplayWindowPresenter.ProcessUiPointerDown();
            _tapToPlayView.Hide();
        }
        
        #endregion
    }
}
