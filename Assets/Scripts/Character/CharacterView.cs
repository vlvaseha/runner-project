using System;
using UnityEngine;

namespace Character
{
    public class CharacterView : MonoBehaviour
    {
        #region Events

        public event Action<string> OnAnimatorEventReceived;

        #endregion
        
        #region Fields

        [SerializeField] private Transform _viewRoot;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private AnimatorEventsReceiver _animatorEventsReceiver;

        #endregion

        #region Properties

        public Animator Animator => _characterAnimator;
        public Transform ViewRoot => _viewRoot;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _animatorEventsReceiver.OnAnimatorEventTriggered += AnimatorEventReceivedHandler;
        }

        private void OnDestroy()
        {
            _animatorEventsReceiver.OnAnimatorEventTriggered -= AnimatorEventReceivedHandler;
        }

        #endregion

        #region Methods

        private void AnimatorEventReceivedHandler(string eventName)
        {
            OnAnimatorEventReceived?.Invoke(eventName);
        }

        #endregion
    }
}
