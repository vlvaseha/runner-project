using System;
using UnityEngine.AddressableAssets;

namespace Managers.Storage
{
    [Serializable]
    public class PrefabInfo 
    {
        public string id;
        public AssetReference assetReference;
    }
}