using System.Collections.Generic;
using Managers;
using Managers.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Levels
{
    public class ChunksController
    {
        private readonly List<LevelChunk> _chunks;
        private readonly AssetInstanceCreator _assetInstanceCreator;
        private readonly PrefabsManager _prefabsManager;
        private readonly Transform _chunksRoot;

        private int _sequenceLength;
        
        public ChunksController(AssetInstanceCreator assetInstanceCreator,
            PrefabsManager prefabsManager,
            Transform chunksRoot)
        {
            _assetInstanceCreator = assetInstanceCreator;
            _prefabsManager = prefabsManager;
            _chunksRoot = chunksRoot;
            _chunks = new List<LevelChunk>();
        }

        public void Create(List<string> chunkIds)
        {
            foreach (string chunkId in chunkIds)
            {
                LevelChunk chunk = CreateChunk(chunkId);
                Vector3 chunkPosition = new Vector3(0f,0f, _sequenceLength);
                chunk.transform.position = chunkPosition;
                
                _sequenceLength += chunk.Length;
                _chunks.Add(chunk);
            }
        }

        private LevelChunk CreateChunk(string id)
        {
            AssetReference chunkAssetReference = _prefabsManager.GetLevelChunkAssetReferenceById(id);
            LevelChunk levelChunk = _assetInstanceCreator.Instantiate<LevelChunk>(chunkAssetReference, _chunksRoot);

            return levelChunk;
        }
    }
}
