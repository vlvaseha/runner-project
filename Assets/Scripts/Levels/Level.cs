using System.Collections.Generic;
using Gameplay;
using GameUi;
using Managers;
using Managers.Storage;
using UnityEngine;

namespace Levels
{
    public class Level : BaseLevel
    {
        #region Fields

        private readonly AssetInstanceCreator _assetInstanceCreator;
        private readonly GameplayWindowPresenter _gameplayWindowPresenter;
        private readonly PrefabsManager _prefabsManager;

        private IPlayerInput _playerInput;
        private Transform _levelRoot;
        private ChunksController _chunksController;

        #endregion

        #region Class lifecycle

        public Level(AssetInstanceCreator assetInstanceCreator,
            GameplayWindowPresenter gameplayWindowPresenter,
            PrefabsManager prefabsManager)
        {
            _assetInstanceCreator = assetInstanceCreator;
            _gameplayWindowPresenter = gameplayWindowPresenter;
            _prefabsManager = prefabsManager;
        }

        #endregion
        
        public override void Initialize()
        {
            List<string> chunksSequence = new List<string>
            {
                "Chunk_01",
                "Chunk_01",
                "Chunk_01",
                "Chunk_05",
                "Chunk_06",
                "Chunk_05",
                "Chunk_02",
                "Chunk_03",
                "Chunk_04",
            };

            _levelRoot = new GameObject(GetType().Name).transform;
            _chunksController = new ChunksController(_assetInstanceCreator, _prefabsManager, _levelRoot);
            
            _gameplayWindowPresenter.StartButtonClicked.AddListener(StartLevel);
            _chunksController.Create(chunksSequence);
        }

        public override void StartLevel()
        {
            _playerInput = _gameplayWindowPresenter.GetPlayerInput();
        }

        public override void CompleteLevel()
        {
        }

        public override void FailLevel()
        {
        }

        public override void DestroyLevel()
        {
            _gameplayWindowPresenter.StartButtonClicked.RemoveListener(StartLevel);
        }
    }
}
