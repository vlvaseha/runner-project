using System;
using DG.Tweening;
using UnityEngine;

namespace Character.States
{
    public class CharacterFlyingState : CharacterMovementState
    {
        #region Fields

        private const float JumpUpDuration = .3f;
        private const float JumpDownDuration = .15f;

        private readonly int _flyingAnimatorTriggerHash;

        private Tweener _lerpFlyingOOffset;
        private Vector3 _flyingOffset;
        private bool _isForwardMovementAvailable;
        private float _flyingInterpolateProgress;

        #endregion

        #region Class lifecycle

        public CharacterFlyingState(CharacterController characterController, CharacterView characterView,
            CharacterStateMachine characterStateMachine) : base(characterController, characterView, characterStateMachine)
        {
            _flyingAnimatorTriggerHash = Animator.StringToHash("Flying");
        }
        
        #endregion

        #region Methods

        public override void Enter()
        {
            _isForwardMovementAvailable = false;
            CharacterView.Animator.SetTrigger(_flyingAnimatorTriggerHash);
        }

        public override void Exit(Action onComplete = null)
        {
            Vector3 flyingOffset = Vector3.up * CharacterController.Data.FlyingHeight;
            LerpFlyingOffset(flyingOffset * _flyingInterpolateProgress, Vector3.zero, JumpDownDuration, () =>
            {
                onComplete?.Invoke();
            });

        }

        public override void LogicUpdate()
        {
            Vector3 characterPosition = CharacterView.transform.position;
            characterPosition.y = 0f;
            
            if (_isForwardMovementAvailable)
            {
                characterPosition = GetPosition(CharacterController.Data.FlyingForwardMoveSpeed);
            }

            characterPosition += _flyingOffset;
            CharacterView.transform.position = characterPosition;
            
            ProcessSideMovement();
        }

        public override void AnimatorEventTriggered(string eventName)
        {
            switch (eventName)
            {
                case CharacterAnimatorEvents.JumpingStarted:
                    Vector3 flyingOffset = Vector3.up * CharacterController.Data.FlyingHeight;
                    LerpFlyingOffset(Vector3.zero, flyingOffset, JumpUpDuration);
                    break;
                case CharacterAnimatorEvents.FlyingAnimationStarted:           
                    _isForwardMovementAvailable = true;
                    break;
            }
        }

        protected override Quaternion GetRotation()
        {
            float maxRotationAngle = CharacterController.Data.ZRotationMaxAngle;
            float rotationSpeed = CharacterController.Data.ZRotationSpeed;
            
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

        #endregion
    }
}