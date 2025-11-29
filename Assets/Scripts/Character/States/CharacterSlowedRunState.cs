using System;
using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Состояние отвечающее за замедленное передвижение персонажа
    /// </summary>
    public class CharacterSlowedRunState : CharacterMovementState
    {
        private const string AnimatorRunningStateName = "Running"; 
        
        private readonly int _runningTriggerHash;
        private readonly int _runningSpeedAnimationHash;

        private IDisposable _exitStateDelayed;

        public CharacterSlowedRunState(CharacterController characterController, CharacterView characterView)
            : base(characterController, characterView)
        {
            _runningTriggerHash = Animator.StringToHash("Running");
            _runningSpeedAnimationHash = Animator.StringToHash("RunningSpeed");
        }

        protected override void OnEnter()
        {
            SetCharacterAnimation();
            CharacterView.Animator.SetFloat(_runningSpeedAnimationHash, CharacterController.CharacterConfig.runningAnimationSpeed);
        }

        protected override void OnExit()
        {
            _exitStateDelayed?.Dispose();
        }

        protected override void OnTick(float dt)
        {
            ProcessForwardMovement(CharacterController.CharacterConfig.slowRunningSpeed);
            ProcessSideMovement();
        }

        private void SetCharacterAnimation()
        {
            AnimatorStateInfo stateInfo = CharacterView.Animator.GetCurrentAnimatorStateInfo(0);

            if (!stateInfo.IsName(AnimatorRunningStateName))
            {
                CharacterView.Animator.SetTrigger(_runningTriggerHash);
            }
        }
    }
}
