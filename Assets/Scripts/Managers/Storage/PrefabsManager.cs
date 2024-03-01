using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Managers.Storage
{
    [CreateAssetMenu(fileName = nameof(PrefabsManager), menuName = "Game/Storage/" + nameof(PrefabsManager))]
    public class PrefabsManager : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private UiPrefabsStorage _uiPrefabsStorage;

        #endregion

        #region Methods

        public AssetReference GetUiAssetReferenceById(string id) => _uiPrefabsStorage.GetPrefabReference(id);
        
        #endregion
    }
}
