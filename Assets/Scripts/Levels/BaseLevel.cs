using UnityEngine;

namespace Levels
{
    public abstract class BaseLevel
    {
        #region Methods

        public abstract void Initialize();

        public abstract void StartLevel();
        
        public abstract void CompleteLevel();
        
        public abstract void FailLevel();
        
        public abstract void DestroyLevel();
        
        #endregion
    }
}
