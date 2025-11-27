using System;
using Gameplay;
using Managers;
using Managers.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameUi
{
    public class GameplayWindowPresenter : BaseWindowPresenter<GameplayWindow>
    {
        public event Action OnStartButtonClicked;
        
        private readonly PrefabsManager _prefabsManager;
        private readonly AssetInstanceCreator _assetInstanceCreator;
        
        public GameplayWindowPresenter(WindowType type, PrefabsManager prefabsManager, AssetInstanceCreator assetInstanceCreator) : base(type)
        {
            _prefabsManager = prefabsManager;
            _assetInstanceCreator = assetInstanceCreator;
        }

        public void StartButtonPressed() => OnStartButtonClicked?.Invoke();

        public IPlayerInput GetPlayerInput()
        {
            Transform inputRoot = IsViewInitialized ? WindowView.transform : null;
            AssetReference assetReference = _prefabsManager.GetUiAssetReferenceById(UiPrefabsIds.UiInput);
            return _assetInstanceCreator.Instantiate<UiPlayerInput>(assetReference, inputRoot);
        }
    }
}
