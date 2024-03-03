using System;
using System.Collections.Generic;
using Character;
using Gameplay;
using GameUi;
using Managers;
using Managers.Storage;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using CharacterController = Character.CharacterController;

namespace Levels
{
    public class Level : BaseLevel
    {
        #region Fields

        private readonly List<string> _chunksSequence = new List<string>
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
            "Chunk_03",
        };
        
        private readonly AssetInstanceCreator _assetInstanceCreator;
        private readonly GameplayWindowPresenter _gameplayWindowPresenter;
        private readonly PrefabsManager _prefabsManager;
        private readonly CameraController _cameraController;

        private CharacterController _characterController;
        private ChunksController _chunksController;
        private Transform _levelRoot;
        private IPlayerInput _playerInput;
        private IDisposable _frameUpdate;

        #endregion

        #region Class lifecycle

        public Level(AssetInstanceCreator assetInstanceCreator,
            GameplayWindowPresenter gameplayWindowPresenter,
            PrefabsManager prefabsManager,
            CameraController cameraController)
        {
            _assetInstanceCreator = assetInstanceCreator;
            _gameplayWindowPresenter = gameplayWindowPresenter;
            _prefabsManager = prefabsManager;
            _cameraController = cameraController;
        }

        #endregion
        
        public override void Initialize()
        {
            _levelRoot = new GameObject(GetType().Name).transform;
            _chunksController = new ChunksController(_assetInstanceCreator, _prefabsManager, _levelRoot);
            
            _gameplayWindowPresenter.StartButtonClicked.AddListener(StartLevel);
            _chunksController.Create(_chunksSequence);
            _characterController = CreateCharacterController();
            _cameraController.CalculateCameraOffset(_characterController.GetViewPosition());
        }

        public override void StartLevel()
        {
            _characterController.Initialize(_gameplayWindowPresenter.GetPlayerInput());
            _frameUpdate = Observable.EveryUpdate().Subscribe(_ => LogicUpdate());
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
            _frameUpdate?.Dispose();
        }

        private void LogicUpdate()
        {
            _characterController.LogicUpdate();
            _cameraController.UpdatePosition(_characterController.GetViewPosition());
            _chunksController.UpdateChunks(_characterController.GetViewPosition());
        }

        private CharacterController CreateCharacterController()
        {
            AssetReference assetReference = _prefabsManager.GetCharacterAssetReferenceById("DefaultCharacter");
            LevelChunk chunk = _chunksController.GetFirstChunk();
            CharacterView characterView = _assetInstanceCreator.Instantiate<CharacterView>(assetReference, _levelRoot);

            characterView.transform.position = chunk.CharacterInitialTransform.position;
            return new CharacterController(characterView);
        }
    }
}
