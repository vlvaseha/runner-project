using System;
using UnityEngine;

namespace PowerUps
{
    /// <summary>
    /// Класс отслеживает какие объеты вошли в триггер, если персонаж игрока - вызывается ивент
    /// </summary>
    public class PowerUpTrigger : MonoBehaviour
    {
        #region Fields

        private bool _isTriggered;

        #endregion
        
        #region Events

        public event Action OnPlayerTriggered;

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
                OnPlayerTriggered?.Invoke();
                _isTriggered = true;
            }
        }

        #endregion
    }
}
