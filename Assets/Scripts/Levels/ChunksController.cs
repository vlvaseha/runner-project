using System.Collections.Generic;
using CollectableBonus;
using Managers;
using Managers.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Levels
{
    public class ChunksController
    {
        #region Fields

        private const float RebuildChunksShift = 13f;

        private readonly List<string> _bonusesId;
        private readonly Queue<LevelChunk> _chunks;
        private readonly AssetInstanceCreator _assetInstanceCreator;
        private readonly PrefabsManager _prefabsManager;
        private readonly Transform _chunksRoot;

        private int _sequenceLength;
        
        #endregion

        #region Class lifecycle

        public ChunksController(AssetInstanceCreator assetInstanceCreator,
            PrefabsManager prefabsManager,
            Transform chunksRoot)
        {
            _assetInstanceCreator = assetInstanceCreator;
            _prefabsManager = prefabsManager;
            _chunksRoot = chunksRoot;
            _chunks = new Queue<LevelChunk>();
            _bonusesId = new List<string>
            {
                CollectableBonusId.FlyingBonus,
                CollectableBonusId.SlowdownBonus,
                CollectableBonusId.SprintRunningBonus
            };
        }
        
        #endregion

        #region Methods

        public void Create(List<string> chunkIds)
        {
            int number = 0;
            
            foreach (string chunkId in chunkIds)
            {
                LevelChunk chunk = CreateChunk(chunkId);
                AddChunk(chunk, ++number > 2);
            }
        }

        public LevelChunk GetFirstChunk() => _chunks.Peek();

        public void UpdateChunks(Vector3 characterPosition)
        {
            var chunk = GetFirstChunk();
            
            if (characterPosition.z > chunk.transform.position.z + RebuildChunksShift)
            {
                chunk = _chunks.Dequeue();
                AddChunk(chunk);
            }
        }

        private void AddChunk(LevelChunk chunk, bool createBonuses = true)
        {
            float zPosition = chunk.Length + _sequenceLength;
            Vector3 chunkPosition = new Vector3(0f,0f, zPosition);
                
            chunk.transform.position = chunkPosition;
                
            _sequenceLength += chunk.Length;
            _chunks.Enqueue(chunk);

            if (createBonuses)
            {
                CreateChunkBonuses(chunk);
            }
        }

        private LevelChunk CreateChunk(string id)
        {
            AssetReference chunkAssetReference = _prefabsManager.GetLevelChunkAssetReferenceById(id);
            LevelChunk levelChunk = _assetInstanceCreator.Instantiate<LevelChunk>(chunkAssetReference, _chunksRoot);

            return levelChunk;
        }

        private void CreateChunkBonuses(LevelChunk chunk)
        {
            chunk.ClearBonuses();
            
            string bonusId = _bonusesId.Random();
            AssetReference bonusAssetReference = _prefabsManager.GetCollectableBonusAssetReferenceById(bonusId);
            BaseCollectableBonus bonus =
                _assetInstanceCreator.Instantiate<BaseCollectableBonus>(bonusAssetReference, chunk.transform);
            
            float zPosition = Random.Range(1f, chunk.Length);
            int xPosition = Random.Range(-2, 3);
            Vector3 localPosition = new Vector3(xPosition, 0f, -zPosition);
            bonus.transform.localPosition = localPosition;
            chunk.AddBonus(bonus);
        }
        
        #endregion
    }
}
