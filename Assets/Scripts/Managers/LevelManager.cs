using System;
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
    public class LevelManager : IInitializable, IDisposable
    {
        private readonly DiContainer _diContainer;
        private readonly AssetInstanceCreator _assetInstanceCreator;
        private readonly PrefabsManager _prefabsManager;
        private readonly StartGameWindowPresenter _startGameWindowPresenter;
        private readonly GameplayWindowPresenter _gameplayWindowPresenter;
        
        private BaseLevel _currentLevel;
        private UiWindows _uiWindows;

        public LevelManager(DiContainer diContainer, AssetInstanceCreator assetInstanceCreator, 
            PrefabsManager prefabsManager, StartGameWindowPresenter startGameWindowPresenter, 
            GameplayWindowPresenter gameplayWindowPresenter)
        {
            _diContainer = diContainer;
            _assetInstanceCreator = assetInstanceCreator;
            _prefabsManager = prefabsManager;
            _startGameWindowPresenter = startGameWindowPresenter;
            _gameplayWindowPresenter = gameplayWindowPresenter;
        }

        void IInitializable.Initialize()
        {
            _uiWindows = CreateUiWindows();
            _uiWindows.Initialize(_assetInstanceCreator, _prefabsManager, _diContainer);

            _currentLevel = CreateLevel();
            _currentLevel.Initialize();
            
            _uiWindows.Open<StartGameWindow>(_startGameWindowPresenter);
            _startGameWindowPresenter.OnStartButtonClicked += StartButtonClickedHandler;
            _gameplayWindowPresenter.OnInitialized += GameplayWindowInitializedHandler;

            Application.targetFrameRate = 60;
        }

        void IDisposable.Dispose()
        {
            _startGameWindowPresenter.OnStartButtonClicked -= StartButtonClickedHandler;
            _gameplayWindowPresenter.OnInitialized -= GameplayWindowInitializedHandler;
        }

        private BaseLevel CreateLevel() =>  _diContainer.Instantiate<Level>();

        private UiWindows CreateUiWindows()
        {
            AssetReference uiWindowsAssetReference = _prefabsManager.GetUiAssetReferenceById(UiPrefabsIds.UiWindows);
            return _assetInstanceCreator.Instantiate<UiWindows>(uiWindowsAssetReference, null);
        }

        private void StartButtonClickedHandler()
        {
            _startGameWindowPresenter.CloseWindow();
            _uiWindows.Open<GameplayWindow>(_gameplayWindowPresenter);
        }

        private void GameplayWindowInitializedHandler() => _currentLevel?.StartLevel();

    }
}
