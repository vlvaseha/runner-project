using UnityEngine;

namespace Character.States
{
    public class CharacterMovementState : BaseCharacterState
    {
        #region Fields
        
        protected Vector2 Input { get; private set; }
        
        private const string AnimatorRunningStateName = "Running"; 
        private const float RunningAnimationSpeed = 1.4f;
        
        private readonly int _runningTriggerHash;
        private readonly int _runningSpeedAnimationHash;
            
        #endregion

        #region Class lifecycle

        public CharacterMovementState(CharacterController characterController, CharacterView characterView,
            CharacterStateMachine characterStateMachine) : base(characterController, characterView,
            characterStateMachine)
        {
            _runningTriggerHash = Animator.StringToHash("Running");
            _runningSpeedAnimationHash = Animator.StringToHash("RunningSpeed");
        }
        
        #endregion

        #region Methods

        public override void Enter()
        {
            SetCharacterAnimation();
            CharacterView.Animator.SetFloat(_runningSpeedAnimationHash, RunningAnimationSpeed);
        }

        public override void UpdateInput(Vector2 input)
        {
            Input = input;
        }

        public override void LogicUpdate()
        {
            ProcessForwardMovement(CharacterController.Data.ForwardMoveSpeed);
            ProcessSideMovement();
        }

        protected void ProcessForwardMovement(float forwardMoveSpeed)
        {
            CharacterView.transform.position = GetPosition(forwardMoveSpeed);
        }

        protected void ProcessSideMovement()
        {
            CharacterView.transform.rotation = GetRotation();
            CharacterView.ViewRoot.localPosition = GetSidePosition();
        }

        protected Vector3 GetPosition(float moveSpeed)
        {
            Vector3 currentMoveForwardPosition = CharacterView.transform.position;
            Vector3 nextMoveForwardPosition =
                currentMoveForwardPosition + Vector3.forward * moveSpeed * Time.deltaTime;

            nextMoveForwardPosition.y = 0f;
            return nextMoveForwardPosition;
        }

        protected Vector3 GetSidePosition()
        {
            float sideOffset = CharacterController.MaxSideMoveOffset;

            Vector3 currentSidePosition = CharacterView.ViewRoot.localPosition;
            Vector3 nextMoveSidePosition =
                currentSidePosition + Input.x * Vector3.right * CharacterController.Data.SideMoveSpeed * Time.deltaTime;

            nextMoveSidePosition.x = Mathf.Clamp(nextMoveSidePosition.x, -sideOffset, sideOffset);
            return nextMoveSidePosition;
        }

        protected virtual Quaternion GetRotation()
        {
            Quaternion targetRotation =
                Quaternion.Euler(new Vector3(0f, CharacterController.Data.YRotationMaxAngle * Input.normalized.x));
            
            return Quaternion.RotateTowards(CharacterView.transform.rotation, targetRotation, CharacterController.Data.YRotationSpeed * Time.deltaTime);
        }

        private void SetCharacterAnimation()
        {
            AnimatorStateInfo stateInfo = CharacterView.Animator.GetCurrentAnimatorStateInfo(0);

            if (!stateInfo.IsName(AnimatorRunningStateName))
            {
                CharacterView.Animator.SetTrigger(_runningTriggerHash);
            }
        }

        #endregion
    }
}
