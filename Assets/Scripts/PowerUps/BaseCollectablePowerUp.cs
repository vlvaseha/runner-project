using CollectableBonus;
using UnityEngine;
using Zenject;

namespace PowerUps
{
    public class BaseCollectablePowerUp : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _powerUpDuration = 10f;
        [SerializeField] private PowerUpTrigger _powerUpTrigger;
        [Space]
        [SerializeField] private GameObject _idleAnimationGO;
        [SerializeField] private GameObject _disappearAnimationGO;

        #endregion

        #region Properties

        protected float PowerUpDuration => _powerUpDuration;
        [Inject] protected SignalBus SignalBus { get; }

        #endregion
    
        #region Unity lifecycle

        private void Start()
        {
            _powerUpTrigger.OnPlayerTriggerd += PlayerTriggeredHandler;
        }

        private void OnDestroy()
        {
            _powerUpTrigger.OnPlayerTriggerd -= PlayerTriggeredHandler;
        }

        #endregion

        #region Methods

        protected virtual void PlayerTriggeredHandler()
        {
            _idleAnimationGO.gameObject.SetActive(false);
            _disappearAnimationGO.gameObject.SetActive(true);
        }

        #endregion
    }
}
