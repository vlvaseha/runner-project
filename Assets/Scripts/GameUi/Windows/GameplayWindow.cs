using Gameplay;
using Managers;
using Managers.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace GameUi
{
    public class GameplayWindow : Window
    {
        #region Fields

        [SerializeField] private UiStartPanel _startPanel;

        [Inject] private AssetInstanceCreator _assetInstanceCreator;
        [Inject] private PrefabsManager _prefabsManager;
        
        private GameplayWindowPresenter _gameplayWindowPresenter;
        private IPlayerInput _playerInput;

        #endregion
        
        #region Methods

        public override void Show()
        {
            _gameplayWindowPresenter = Arguments as GameplayWindowPresenter;
            _gameplayWindowPresenter.Initialize(this);
            
            _startPanel.StartButtonClicked.AddListener(StartButtonClickedHandler);
        }

        public override void Hide()
        {
            _startPanel.StartButtonClicked.RemoveListener(StartButtonClickedHandler);
        }

        public IPlayerInput CreateUiPlayerInput()
        {
            AssetReference assetReference = _prefabsManager.GetUiAssetReferenceById(UiPrefabsIds.UiInput);

            _playerInput = _assetInstanceCreator.Instantiate<UiPlayerInput>(assetReference, transform);
            return _playerInput;
        }

        private void StartButtonClickedHandler()
        {
            _gameplayWindowPresenter.StartButtonPressed();
        }
        
        #endregion
    }
}
