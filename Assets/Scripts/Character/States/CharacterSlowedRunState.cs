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
        private const float RunningAnimationSpeed = .5f;
        
        private readonly int _runningTriggerHash;
        private readonly int _runningSpeedAnimationHash;
        private readonly float _slowedRunSpeed;

        private IDisposable _exitStateDelayed;

        public CharacterSlowedRunState(CharacterController characterController, CharacterView characterView, float slowedRunSpeed)
            : base(characterController, characterView)
        {
            _slowedRunSpeed = slowedRunSpeed;
            _runningTriggerHash = Animator.StringToHash("Running");
            _runningSpeedAnimationHash = Animator.StringToHash("RunningSpeed");
        }

        protected override void OnEnter()
        {
            SetCharacterAnimation();
            CharacterView.Animator.SetFloat(_runningSpeedAnimationHash, RunningAnimationSpeed);
        }

        protected override void OnExit()
        {
            _exitStateDelayed?.Dispose();
        }

        protected override void OnTick(float dt)
        {
            ProcessForwardMovement(_slowedRunSpeed);
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
