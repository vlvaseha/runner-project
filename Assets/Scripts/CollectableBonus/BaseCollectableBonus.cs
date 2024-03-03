using UnityEngine;

namespace CollectableBonus
{
    public class BaseCollectableBonus : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BonusTrigger _bonusTrigger;
        [Space]
        [SerializeField] private GameObject _idleAnimationGO;
        [SerializeField] private GameObject _disappearAnimationGO;

        #endregion
    
        #region Unity lifecycle

        private void Start()
        {
            _bonusTrigger.OnPlayerTriggerd += PlayerTriggeredHandler;
        }

        private void OnDestroy()
        {
            _bonusTrigger.OnPlayerTriggerd -= PlayerTriggeredHandler;
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
