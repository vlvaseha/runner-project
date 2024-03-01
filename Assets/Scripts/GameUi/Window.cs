using System;
using UnityEngine;

namespace GameUi
{
    public abstract class Window : MonoBehaviour
    {
        #region Properties

        protected WindowArguments Arguments { get; private set; }

        #endregion
        
        #region Methods

        public void Initialize(WindowArguments arguments) => Arguments = arguments;

        public abstract void Show(bool immediately = false, Action onComplete = null);
        
        public abstract void Hide(bool immediately = false, Action onComplete = null);

        #endregion
    }
}
