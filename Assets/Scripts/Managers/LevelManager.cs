using GameUi;
using Levels;
using Managers.Storage;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Managers
{
    public class LevelManager : IInitializable
    {
        #region Fields

        private readonly DiContainer _diContainer;
        private readonly AssetInstanceCreator _assetInstanceCreator;
        private readonly PrefabsManager _prefabsManager;
        
        private BaseLevel _currentLevel;
        private UiWindows _uiWindows;

        #endregion
        
        #region Class lifecycle

        public LevelManager(DiContainer diContainer,
            AssetInstanceCreator assetInstanceCreator,
            PrefabsManager prefabsManager)
        {
            _diContainer = diContainer;
            _assetInstanceCreator = assetInstanceCreator;
            _prefabsManager = prefabsManager;
        }

        void IInitializable.Initialize()
        {
            _uiWindows = CreateUiWindows();
            _uiWindows.Initialize(_assetInstanceCreator, _prefabsManager, _diContainer);

            _currentLevel = CreateLevel();
            _currentLevel.Initialize();

            var gameplayWindowPresenter = UiServiceContainer.Instance.GameplayWindowPresenter;
            gameplayWindowPresenter.OnPointerDown.AddListener(UiPointerDownHandler);
            
            _uiWindows.Open<GameplayWindow>(gameplayWindowPresenter);
        }
        
        #endregion

        #region Methods

        private void UiPointerDownHandler()
        {
            var gameplayWindowPresenter = UiServiceContainer.Instance.GameplayWindowPresenter;
            gameplayWindowPresenter.OnPointerDown.RemoveListener(_currentLevel.StartLevel);

            _currentLevel.StartLevel();
        }

        private BaseLevel CreateLevel()
        {
            return _diContainer.Instantiate<Level>();
        }

        private UiWindows CreateUiWindows()
        {
            AssetReference uiWindowsAssetReference = _prefabsManager.GetUiWindowsReferenceById(UiPrefabsIds.UiWindows);
            return _assetInstanceCreator.Instantiate<UiWindows>(uiWindowsAssetReference, null);
        }

        #endregion
        
    }
}
