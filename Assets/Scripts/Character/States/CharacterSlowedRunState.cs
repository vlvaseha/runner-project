using System;
using UniRx;
using UnityEngine;

namespace Character.States
{
    public class CharacterSlowedRunState : CharacterMovementState
    {
        #region Fields

        private const string AnimatorRunningStateName = "Running"; 
        private const float RunningAnimationSpeed = .5f;
        
        private readonly int _runningTriggerHash;
        private readonly int _runningSpeedAnimationHash;
        private readonly float _slowedRunSpeed;
        private readonly float _stateDuration;

        private IDisposable _exitStateDelayed;

        #endregion

        #region Class lifecycle

        public CharacterSlowedRunState(CharacterController characterController, CharacterView characterView,
            CharacterStateMachine characterStateMachine, float slowedRunSpeed, float stateDuration)
            : base(characterController, characterView, characterStateMachine)
        {
            _slowedRunSpeed = slowedRunSpeed;
            _stateDuration = stateDuration;

            _runningTriggerHash = Animator.StringToHash("Running");
            _runningSpeedAnimationHash = Animator.StringToHash("RunningSpeed");
        }
        
        #endregion

        #region Methods

        public override void Enter()
        {
            SetCharacterAnimation();
            CharacterView.Animator.SetFloat(_runningSpeedAnimationHash, RunningAnimationSpeed);
            
            _exitStateDelayed = Observable
                .Timer(TimeSpan.FromSeconds(_stateDuration))
                .Subscribe(_ => ExitStateDelayed())
                .AddTo(CharacterView);
        }

        public override void Exit(Action onComplete = null)
        {
            _exitStateDelayed?.Dispose();
            onComplete?.Invoke();
        }

        public override void LogicUpdate()
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

        private void ExitStateDelayed()
        {
            CharacterStateMachine.ChangeState(new CharacterMovementState(CharacterController, CharacterView, CharacterStateMachine));
        }

        #endregion
    }
}
