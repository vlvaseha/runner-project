using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Managers.Storage
{
    public class BasePrefabsStorage : ScriptableObject
    {
        [FormerlySerializedAs("_prefabs")] [SerializeField] private List<PrefabInfo> prefabs;

        public AssetReference GetPrefabReference(string id)
        {
            PrefabInfo prefabInfo = prefabs.Find(info => info.id == id);
            return prefabInfo.assetReference;
        }
    }
}