using System;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Класс предназначен для отслеживания ивентов анимаций
    /// </summary>
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
