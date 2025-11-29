using System;
using PowerUps;
using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Состояние отвечающее за ускоренное передвижение персонажа
    /// </summary>
    public class CharacterSprintRunState : CharacterMovementState
    {
        private const string AnimatorSprintStateName = "SprintRunning"; 
        
        private readonly SprintPowerUpConfig _sprintPowerUpConfig;
        private readonly int _sprintRunningTriggerHash;

        private IDisposable _exitStateDelayed;

        public CharacterSprintRunState(CharacterView characterView, CharacterMovementSettings characterMovementSettings,
            BasePowerUpConfig basePowerUpConfig) : base(characterView, characterMovementSettings)
        {
            _sprintPowerUpConfig = (SprintPowerUpConfig) basePowerUpConfig;
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
            ProcessForwardMovement(_sprintPowerUpConfig.springRunningSpeed);
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
