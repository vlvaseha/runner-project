using System;
using UniRx;
using UnityEngine;

namespace Character.States
{
    
    /// <summary>
    /// Состояние отвечающее за ускоренное передвижение персонажа
    /// </summary>
    public class CharacterSprintRunState : CharacterMovementState
    {
        #region Fields

        private const string AnimatorSprintStateName = "SprintRunning"; 
        
        private readonly int _sprintRunningTriggerHash;
        private readonly float _sprintSpeed;
        private readonly float _stateDuration;

        private IDisposable _exitStateDelayed;

        #endregion

        #region Class lifecycle

        public CharacterSprintRunState(CharacterController characterController, CharacterView characterView,
            CharacterStateMachine characterStateMachine, float sprintSpeed, float stateDuration)
            : base(characterController, characterView, characterStateMachine)
        {
            _sprintSpeed = sprintSpeed;
            _stateDuration = stateDuration;

            _sprintRunningTriggerHash = Animator.StringToHash("SprintRunning");
        }
        
        #endregion

        #region Methods

        public override void Enter()
        {
            SetCharacterAnimation();
            
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

        private void ExitStateDelayed()
        {
            CharacterStateMachine.ChangeState(new CharacterMovementState(CharacterController, CharacterView, CharacterStateMachine));
        }

        #endregion
    }
}
