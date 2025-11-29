using System;
using DG.Tweening;
using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Состояние в котором персонаж осуществляет полет
    /// </summary>
    public class CharacterFlyingState : CharacterMovementState
    {
        private readonly int _flyingAnimatorTriggerHash;

        private Tweener _lerpFlyingOOffset;
        private Vector3 _flyingOffset;
        private bool _isForwardMovementAvailable;
        private float _flyingInterpolateProgress;

        private IDisposable _exitStateDelayed;

        public CharacterFlyingState(CharacterController characterController, CharacterView characterView)
            : base(characterController, characterView)
        {
            _flyingAnimatorTriggerHash = Animator.StringToHash("Flying");
        }

        protected override void OnEnter()
        {
            _isForwardMovementAvailable = false;
            CharacterView.Animator.SetTrigger(_flyingAnimatorTriggerHash);
        }

        protected override void OnExit()
        {
            _exitStateDelayed?.Dispose();
            
            Vector3 flyingOffset = Vector3.up * CharacterController.CharacterConfig.flyingHeight;
            LerpFlyingOffset(flyingOffset * _flyingInterpolateProgress, Vector3.zero, CharacterController.CharacterConfig.jumpDownDuration);
        }

        protected override void OnTick(float dt)
        {
            Vector3 characterPosition = CharacterView.transform.position;
            characterPosition.y = 0f;
            
            if (_isForwardMovementAvailable)
            {
                characterPosition = GetPosition(CharacterController.CharacterConfig.flyingForwardMoveSpeed);
            }

            characterPosition += _flyingOffset;
            CharacterView.transform.position = characterPosition;
            
            ProcessSideMovement();
        }

        protected override void OnAnimatorEventTriggered(string eventName)
        {
            switch (eventName)
            {
                case CharacterAnimatorEvents.JumpingStarted:
                    Vector3 flyingOffset = Vector3.up * CharacterController.CharacterConfig.flyingHeight;
                    LerpFlyingOffset(Vector3.zero, flyingOffset, CharacterController.CharacterConfig.jumpUpDuration);
                    break;
                case CharacterAnimatorEvents.FlyingAnimationStarted:           
                    _isForwardMovementAvailable = true;
                    break;
            }
        }

        protected override Quaternion GetRotation()
        {
            float maxRotationAngle = CharacterController.CharacterConfig.zRotationMaxAngle;
            float rotationSpeed = CharacterController.CharacterConfig.zRotationSpeed;
            
            Quaternion targetRotation =
                Quaternion.Euler(new Vector3(0f, 0f, -maxRotationAngle * Input.normalized.x));
            
            return Quaternion.RotateTowards(CharacterView.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void LerpFlyingOffset(Vector3 from, Vector3 to, float duration, Action onComplete = null)
        {
            float startInterpolateValue = 1f - _flyingInterpolateProgress;
            float interpolateDuration = duration * _flyingInterpolateProgress;
            
            _lerpFlyingOOffset?.Kill();
            _lerpFlyingOOffset = DOVirtual
                .Float(startInterpolateValue, 1f, interpolateDuration, value =>
                {
                    _flyingInterpolateProgress = value;
                    _flyingOffset = Vector3.Lerp(from, to, value);
                })
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}
