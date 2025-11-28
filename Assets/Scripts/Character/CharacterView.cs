using System;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Класс отвечающий за отображение персонажа и взаимодействие с компнонентами юнити
    /// </summary>
    public class CharacterView : MonoBehaviour
    {
        public event Action<string> OnAnimatorEventReceived;
        
        [SerializeField] private Transform viewRoot;
        [SerializeField] private Animator characterAnimator;
        [SerializeField] private AnimatorEventsReceiver animatorEventsReceiver;

        public Animator Animator => characterAnimator;
        public Transform ViewRoot => viewRoot;

        private void Start()
        {
            animatorEventsReceiver.OnAnimatorEventTriggered += AnimatorEventReceivedHandler;
        }

        private void OnDestroy()
        {
            animatorEventsReceiver.OnAnimatorEventTriggered -= AnimatorEventReceivedHandler;
        }

        private void AnimatorEventReceivedHandler(string eventName)
        {
            OnAnimatorEventReceived?.Invoke(eventName);
        }
    }
}
