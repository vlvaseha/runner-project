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
using Zenject;
using CharacterController = Character.CharacterController;

namespace Levels
{
    /// <summary>
    /// Класс уровня, в классе создается уровень, инциализирует объекты которые создают уровень,
    /// а так же содержит логику опеределяющую пройден/проигран уровень 
    /// </summary>
    public class Level : BaseLevel
    {
        private const string CharacterPrefabId = "DefaultCharacter";
        
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
        private readonly SignalBus _signalBus;
        private readonly DiContainer _diContainer;

        private CharacterController _characterController;
        private ChunksController _chunksController;
        private Transform _levelRoot;
        private IPlayerInput _playerInput;
        private IDisposable _frameUpdate;

        public Level(AssetInstanceCreator assetInstanceCreator, PrefabsManager prefabsManager,
            CameraController cameraController, SignalBus signalBus, GameplayWindowPresenter gameplayWindowPresenter, 
            DiContainer diContainer)
        {
            _assetInstanceCreator = assetInstanceCreator;
            _prefabsManager = prefabsManager;
            _cameraController = cameraController;
            _signalBus = signalBus;
            _gameplayWindowPresenter = gameplayWindowPresenter;
            _diContainer = diContainer;
        }
        
        public override void Initialize()
        {
            _levelRoot = new GameObject(GetType().Name).transform;
            _chunksController = new ChunksController(_assetInstanceCreator, _prefabsManager, _levelRoot);
            
            _chunksController.Create(_chunksSequence);
            _characterController = CreateCharacterController();
            _cameraController.CalculateCameraOffset(_characterController.GetViewPosition());
        }

        public override void StartLevel()
        {
            if (_gameplayWindowPresenter.TryGetInputPanel(out IPlayerInput inputPanel))
            {
                _characterController.OnLevelStarted(inputPanel);
            }
            
            _frameUpdate = Observable.EveryUpdate().Subscribe(_ => LogicUpdate());
        }

        public override void CompleteLevel() { }

        public override void FailLevel() { }

        public override void DestroyLevel()
        {
            _frameUpdate?.Dispose();
            _characterController?.Dispose();
        }

        private void LogicUpdate()
        {
            _characterController.LogicUpdate();
            _cameraController.UpdatePosition(_characterController.GetViewPosition());
            _chunksController.UpdateChunks(_characterController.GetViewPosition());
        }

        private CharacterController CreateCharacterController()
        {
            AssetReference assetReference = _prefabsManager.GetCharacterAssetReferenceById(CharacterPrefabId);
            LevelChunk chunk = _chunksController.GetFirstChunk();
            CharacterView characterView = _assetInstanceCreator.Instantiate<CharacterView>(assetReference, _levelRoot);

            characterView.transform.position = chunk.CharacterInitialTransform.position;
            CharacterController characterController = new CharacterController(characterView, _signalBus, _diContainer);
            characterController.Initialize();
            
            return characterController;
        }
    }
}
