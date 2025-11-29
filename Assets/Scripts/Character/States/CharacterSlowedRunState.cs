using System;
using PowerUps;
using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Состояние отвечающее за замедленное передвижение персонажа
    /// </summary>
    public class CharacterSlowedRunState : CharacterMovementState
    {
        private const string AnimatorRunningStateName = "Running"; 
        
        private readonly SlowDownPowerUpConfig _slowDownPowerUpConfig;
        private readonly int _runningTriggerHash;
        private readonly int _runningSpeedAnimationHash;

        private IDisposable _exitStateDelayed;

        public CharacterSlowedRunState(CharacterView characterView, CharacterMovementSettings characterMovementSettings,
            BasePowerUpConfig basePowerUpConfig) : base(characterView, characterMovementSettings)
        {
            _slowDownPowerUpConfig = (SlowDownPowerUpConfig) basePowerUpConfig;
            _runningTriggerHash = Animator.StringToHash("Running");
            _runningSpeedAnimationHash = Animator.StringToHash("RunningSpeed");
        }

        protected override void OnEnter()
        {
            SetCharacterAnimation();
            CharacterView.Animator.SetFloat(_runningSpeedAnimationHash, _slowDownPowerUpConfig.runningAnimationSpeed);
        }

        protected override void OnExit()
        {
            _exitStateDelayed?.Dispose();
        }

        protected override void OnTick(float dt)
        {
            ProcessForwardMovement(_slowDownPowerUpConfig.slowRunningSpeed);
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
