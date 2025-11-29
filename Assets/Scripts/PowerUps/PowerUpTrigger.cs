using System;
using UnityEngine;

namespace PowerUps
{
    /// <summary>
    /// Класс отслеживает какие объеты вошли в триггер, если персонаж игрока - вызывается ивент
    /// </summary>
    public class PowerUpTrigger : MonoBehaviour
    {
        private bool _isTriggered;
        
        public event Action OnPlayerTriggered;

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
    }
}
