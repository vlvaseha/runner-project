using Managers;
using UnityEngine;

namespace Levels
{
    public class Level : BaseLevel
    {
        #region Fields

        private readonly AssetInstanceCreator _assetInstanceCreator;
        
        private Transform _levelRoot;

        #endregion

        #region Class lifecycle

        public Level(AssetInstanceCreator assetInstanceCreator)
        {
            
        }

        #endregion
        
        public override void Initialize()
        {
            _levelRoot = new GameObject(GetType().Name).transform;
        }

        public override void StartLevel()
        {
            throw new System.NotImplementedException();
        }

        public override void CompleteLevel()
        {
            throw new System.NotImplementedException();
        }

        public override void FailLevel()
        {
            throw new System.NotImplementedException();
        }

        public override void DestroyLevel()
        {
            throw new System.NotImplementedException();
        }
    }
}
