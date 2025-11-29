using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Состояние отвечающее за дефолтное передвижение персонажа
    /// </summary>
    public class CharacterMovementState : BaseCharacterState<CharacterMovementState>
    {
        protected Vector2 Input { get; private set; }
        
        private const string AnimatorRunningStateName = "Running"; 
        
        private readonly int _runningTriggerHash;
        private readonly int _runningSpeedAnimationHash;

        public CharacterMovementState(CharacterController characterController, CharacterView characterView)
            : base(characterController, characterView)
        {
            _runningTriggerHash = Animator.StringToHash("Running");
            _runningSpeedAnimationHash = Animator.StringToHash("RunningSpeed");
        }
        
        protected override void OnEnter()
        {
            SetCharacterAnimation();
            CharacterView.Animator.SetFloat(_runningSpeedAnimationHash, CharacterController.CharacterConfig.runningAnimationMultiplier);
        }

        protected override void OnInputUpdated(Vector2 input)
        {
            Input = input;
        }

        protected override void OnTick(float deltaTime)
        {
            ProcessForwardMovement(CharacterController.CharacterConfig.forwardMoveSpeed);
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

        private Vector3 GetSidePosition()
        {
            float sideOffset = CharacterController.CharacterConfig.maxSideMovementOffset;

            Vector3 currentSidePosition = CharacterView.ViewRoot.localPosition;
            Vector3 nextMoveSidePosition =
                currentSidePosition + Input.x * Vector3.right * CharacterController.CharacterConfig.sideMoveSpeed * Time.deltaTime;

            nextMoveSidePosition.x = Mathf.Clamp(nextMoveSidePosition.x, -sideOffset, sideOffset);
            return nextMoveSidePosition;
        }

        protected virtual Quaternion GetRotation()
        {
            Quaternion targetRotation =
                Quaternion.Euler(new Vector3(0f, CharacterController.CharacterConfig.yRotationMaxAngle * Input.normalized.x));
            
            return Quaternion.RotateTowards(CharacterView.transform.rotation, targetRotation, CharacterController.CharacterConfig.yRotationSpeed * Time.deltaTime);
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
