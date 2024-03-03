using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Managers.Storage
{
    [CreateAssetMenu(fileName = nameof(PrefabsManager), menuName = "Game/Storage/" + nameof(PrefabsManager))]
    public class PrefabsManager : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private UiPrefabsStorage _uiPrefabsStorage;
        [SerializeField] private ChunksStorage _chunksStorage;
        [SerializeField] private CharactersStorage _charactersStorage;
        [SerializeField] private CollectableBonusStorage _collectableBonusStorage;

        #endregion

        #region Methods

        public AssetReference GetUiAssetReferenceById(string id) => _uiPrefabsStorage.GetPrefabReference(id);
        
        public AssetReference GetLevelChunkAssetReferenceById(string id) => _chunksStorage.GetPrefabReference(id);
        
        public AssetReference GetCharacterAssetReferenceById(string id) => _charactersStorage.GetPrefabReference(id);

        public AssetReference GetCollectableBonusAssetReferenceById(string id) =>
            _collectableBonusStorage.GetPrefabReference(id);

        #endregion
    }
}
