using System.Collections.Generic;
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
        }
        
        #endregion

        #region Methods

        public void Create(List<string> chunkIds)
        {
            foreach (string chunkId in chunkIds)
            {
                LevelChunk chunk = CreateChunk(chunkId);
                AddChunk(chunk);
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

        private void AddChunk(LevelChunk chunk)
        {
            float zPosition = chunk.Length + _sequenceLength;
            Vector3 chunkPosition = new Vector3(0f,0f, zPosition);
                
            chunk.transform.position = chunkPosition;
                
            _sequenceLength += chunk.Length;
            _chunks.Enqueue(chunk);
        }

        private LevelChunk CreateChunk(string id)
        {
            AssetReference chunkAssetReference = _prefabsManager.GetLevelChunkAssetReferenceById(id);
            LevelChunk levelChunk = _assetInstanceCreator.Instantiate<LevelChunk>(chunkAssetReference, _chunksRoot);

            return levelChunk;
        }
        
        #endregion
    }
}
