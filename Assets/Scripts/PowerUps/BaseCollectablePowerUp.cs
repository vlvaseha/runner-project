using UnityEngine;
using Zenject;

namespace PowerUps
{
    /// <summary>
    /// Базовый класс объектов которые изменяеют поведение персонажа, содержит общие для них всех данные
    /// </summary>
    public class BaseCollectablePowerUp : MonoBehaviour
    {
        [SerializeField] private float _powerUpDuration = 10f;
        [SerializeField] private PowerUpTrigger _powerUpTrigger;
        [Space]
        [SerializeField] private GameObject _idleAnimationGO;
        [SerializeField] private GameObject _disappearAnimationGO;
        [SerializeField] private string sdf;

        protected SignalBus SignalBus { get; set; }
        protected BasePowerUpConfig PowerUpConfig { get; set; }
        
        private void Start()
        {
            _powerUpTrigger.OnPlayerTriggered += PlayerTriggeredHandler;
        }

        private void OnDestroy()
        {
            _powerUpTrigger.OnPlayerTriggered -= PlayerTriggeredHandler;
        }

        protected virtual void PlayerTriggeredHandler()
        {
            _idleAnimationGO.gameObject.SetActive(false);
            _disappearAnimationGO.gameObject.SetActive(true);
        }
    }
}
