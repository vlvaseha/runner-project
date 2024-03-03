using System;
using UnityEngine;

namespace CollectableBonus
{
    public class PowerUpTrigger : MonoBehaviour
    {
        #region Fields

        private bool _isTriggered;

        #endregion
        
        #region Events

        public event Action OnPlayerTriggerd;

        #endregion

        #region Class lifecycle

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered)
            {
                return;
            }
            
            if (other.CompareTag("Player"))
            {
                OnPlayerTriggerd?.Invoke();
                _isTriggered = true;
            }
        }

        #endregion
    }
}
