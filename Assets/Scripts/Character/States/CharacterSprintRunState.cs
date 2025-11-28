using System;
using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Состояние отвечающее за ускоренное передвижение персонажа
    /// </summary>
    public class CharacterSprintRunState : CharacterMovementState
    {
        private const string AnimatorSprintStateName = "SprintRunning"; 
        
        private readonly int _sprintRunningTriggerHash;
        private readonly float _sprintSpeed;

        private IDisposable _exitStateDelayed;

        public CharacterSprintRunState(CharacterController characterController, CharacterView characterView,
            float sprintSpeed) : base(characterController, characterView)
        {
            _sprintSpeed = sprintSpeed;
            _sprintRunningTriggerHash = Animator.StringToHash("SprintRunning");
        }

        protected override void OnEnter()
        {
            SetCharacterAnimation();
        }

        protected override void OnExit()
        {
            _exitStateDelayed?.Dispose();
        }

        protected override void OnTick(float dt)
        {
            ProcessForwardMovement(_sprintSpeed);
            ProcessSideMovement();
        }

        private void SetCharacterAnimation()
        {
            AnimatorStateInfo stateInfo = CharacterView.Animator.GetCurrentAnimatorStateInfo(0);

            if (!stateInfo.IsName(AnimatorSprintStateName))
            {
                CharacterView.Animator.SetTrigger(_sprintRunningTriggerHash);
            }
        }
    }
}
