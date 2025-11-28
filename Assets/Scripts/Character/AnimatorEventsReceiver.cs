using System;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Класс предназначен для отслеживания ивентов анимаций
    /// </summary>
    public class AnimatorEventsReceiver : MonoBehaviour
    {
        public event Action<string> OnAnimatorEventTriggered;

        private void FlyingAnimationStarted()
        {
            OnAnimatorEventTriggered?.Invoke(CharacterAnimatorEvents.FlyingAnimationStarted);
        }

        private void JumpingStarted()
        {
            OnAnimatorEventTriggered?.Invoke(CharacterAnimatorEvents.JumpingStarted);
        }
    }
}
