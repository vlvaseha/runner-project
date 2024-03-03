using System;
using UnityEngine;

namespace CollectableBonus
{
    public class BonusTrigger : MonoBehaviour
    {
        #region Events

        public event Action OnPlayerTriggerd;

        #endregion

        #region Class lifecycle

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerTriggerd?.Invoke();
            }
        }

        #endregion
    }
}
