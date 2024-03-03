using System;
using UnityEngine;

namespace Character
{
    public class AnimatorEventsReceiver : MonoBehaviour
    {
        #region Events

        public event Action<string> OnAnimatorEventTriggered;

        #endregion

        #region Animator events handlers

        
        private void FlyingAnimationStarted()
        {
            OnAnimatorEventTriggered?.Invoke(CharacterAnimatorEvents.FlyingAnimationStarted);
        }

        private void JumpingStarted()
        {
            OnAnimatorEventTriggered?.Invoke(CharacterAnimatorEvents.JumpingStarted);
        }
        
        #endregion
    }
}
