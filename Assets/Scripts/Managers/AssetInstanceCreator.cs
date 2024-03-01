using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Object = UnityEngine.Object;

namespace Managers
{
    public class AssetInstanceCreator : IDisposable
    {
        #region Fields

        private readonly DiContainer _diContainer;
        private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> _loadedAssetsMap;
        
        #endregion

        #region Class lifecycle

        public AssetInstanceCreator(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _loadedAssetsMap = new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();
        }

        public void Dispose()
        {
            foreach (var asyncOperationHandle in _loadedAssetsMap.Values)
            {
                Addressables.Release(asyncOperationHandle);
            }
        }

        #endregion

        #region Methods

        public T Instantiate<T>(AssetReference assetReference, Transform parent)
        {
            GameObject assetPrefab = GetPrefab(assetReference);
            T newObj = Object.Instantiate(assetPrefab, parent).GetComponent<T>();
            
            _diContainer.Inject(newObj);

            return newObj;
        }

        private GameObject GetPrefab(AssetReference assetReference)
        {
            if (_loadedAssetsMap.TryGetValue(assetReference, out AsyncOperationHandle<GameObject> loadingHandler))
            {
                return loadingHandler.Result;
            }
            
            AsyncOperationHandle<GameObject> loadAssetAsyncHandler = assetReference.LoadAssetAsync<GameObject>();
            loadAssetAsyncHandler.WaitForCompletion();
            
            _loadedAssetsMap.Add(assetReference, loadAssetAsyncHandler);
            return loadAssetAsyncHandler.Result;
        }

        #endregion
    }
}
