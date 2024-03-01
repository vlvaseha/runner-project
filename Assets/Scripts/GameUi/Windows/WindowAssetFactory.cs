using System;
using Managers;
using Managers.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace GameUi
{
    public class WindowAssetFactory : WindowFactory
    {
        #region Fields

        private readonly DiContainer _diContainer;
        private readonly PrefabsManager _gameAssetData;
        private readonly AssetInstanceCreator _assetInstanceCreator;
        
        #endregion

        #region Class lifecycle

        public WindowAssetFactory(
            DiContainer diContainer,
            PrefabsManager gameAssetData,
            AssetInstanceCreator assetInstanceCreator)
        {
            _diContainer = diContainer;
            _gameAssetData = gameAssetData;
            _assetInstanceCreator = assetInstanceCreator;
        }
        
        #endregion

        #region Methods

        public override Window Create(Type windowType, Transform windowRoot)
        {
            string windowName = windowType.Name;

            AssetReference windowObjectRef = _gameAssetData.GetUiAssetReferenceById(windowName);

            if (windowObjectRef == null)
            {
                Debug.LogWarning("Warning! No prefab found in resources");
                return null;
            }

            Window window = _assetInstanceCreator.Instantiate<Window>(windowObjectRef, windowRoot);
            _diContainer.InjectGameObject(window.gameObject);
            
            return window;
        }
        
        #endregion
    }
}