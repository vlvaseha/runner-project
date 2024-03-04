using GameUi;
using Levels;
using Managers.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Managers
{
    /// <summary>
    /// Класс содержащий логику спавна нужного уровня
    /// </summary>
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

            GameplayWindowPresenter gameplayWindowPresenter = UiServiceContainer.Instance.GameplayWindowPresenter;
            _uiWindows.Open<GameplayWindow>(gameplayWindowPresenter);

            Application.targetFrameRate = 60;
        }
        
        #endregion

        #region Methods

        private BaseLevel CreateLevel()
        {
            return _diContainer.Instantiate<Level>();
        }

        private UiWindows CreateUiWindows()
        {
            AssetReference uiWindowsAssetReference = _prefabsManager.GetUiAssetReferenceById(UiPrefabsIds.UiWindows);
            return _assetInstanceCreator.Instantiate<UiWindows>(uiWindowsAssetReference, null);
        }

        #endregion
        
    }
}
