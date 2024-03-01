using System;
using Managers;
using Managers.Storage;
using UnityEngine;
using Zenject;

namespace GameUi
{
    public class UiWindows : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _windowsRoot;
        
        private WindowFactory _windowFactory;

        #endregion

        #region Methods

        public void Initialize(AssetInstanceCreator assetInstanceCreator, PrefabsManager prefabsManager, DiContainer diContainer)
        {
            _windowFactory = new WindowAssetFactory(diContainer, prefabsManager, assetInstanceCreator);
        }

        public void Open<T>(WindowArguments arguments)
        {
            Window window = _windowFactory.Create(typeof(T), _windowsRoot);
            window.Initialize(arguments);
            window.Show();
        }

        #endregion
    }
}
