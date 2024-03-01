using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Managers.Storage
{
    public class BasePrefabsStorage : ScriptableObject
    {
        #region Fields

        [SerializeField, TableList] private List<PrefabInfo> _prefabs;

        #endregion

        #region Methods

        public AssetReference GetPrefabReference(string id)
        {
            PrefabInfo prefabInfo = _prefabs.Find(info => info.id == id);
            return prefabInfo.assetReference;
        }

        #endregion
    }
}