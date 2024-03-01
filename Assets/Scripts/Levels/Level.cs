using Gameplay;
using GameUi;
using Managers;
using UnityEngine;

namespace Levels
{
    public class Level : BaseLevel
    {
        #region Fields

        private readonly AssetInstanceCreator _assetInstanceCreator;
        private readonly GameplayWindowPresenter _gameplayWindowPresenter;

        private IPlayerInput _playerInput;
        private Transform _levelRoot;

        #endregion

        #region Class lifecycle

        public Level(AssetInstanceCreator assetInstanceCreator,
            GameplayWindowPresenter gameplayWindowPresenter)
        {
            _assetInstanceCreator = assetInstanceCreator;
            _gameplayWindowPresenter = gameplayWindowPresenter;
        }

        #endregion
        
        public override void Initialize()
        {
            _levelRoot = new GameObject(GetType().Name).transform;
            _gameplayWindowPresenter.StartButtonClicked.AddListener(StartLevel);
        }

        public override void StartLevel()
        {
            Debug.Log("StartLevel");
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
