using UnityEngine;

namespace Character.States
{
    public class CharacterMovementState : BaseCharacterState
    {
        #region Fields

        private readonly int RunningTriggerHash = Animator.StringToHash("Running");

        private Vector2 _input;

        private float _forwardSpeed = 4f;
        private float _sideSpeed = 18f;
            
        #endregion

        #region Class lifecycle

        public CharacterMovementState(CharacterController characterController, CharacterView characterView) 
            : base(characterController, characterView) { }
        
        #endregion

        #region Methods

        public override void Enter()
        {
            CharacterView.Animator.SetTrigger(RunningTriggerHash);
        }

        public override void UpdateInput(Vector2 input)
        {
            _input = input;
        }

        public override void LogicUpdate()
        {
            Vector3 currentMoveForwardPosition = CharacterView.transform.position;
            Vector3 nextMoveForwardPosition =
                currentMoveForwardPosition + Vector3.forward * _forwardSpeed * Time.deltaTime;

            CharacterView.transform.position = nextMoveForwardPosition;

            Vector3 currentSidePosition = CharacterView.ViewRoot.localPosition;
            Vector3 nextMoveSidePosition =
                currentSidePosition + _input.x * Vector3.right * _sideSpeed * Time.deltaTime;

            nextMoveSidePosition.x = Mathf.Clamp(nextMoveSidePosition.x, -2.5f, 2.5f);
            CharacterView.ViewRoot.localPosition = nextMoveSidePosition;
            Rotate();
        }

        private void Rotate()
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 12 * _input.normalized.x));
            CharacterView.transform.rotation = Quaternion.RotateTowards(CharacterView.transform.rotation, targetRotation, 90 * Time.deltaTime);
        }

        #endregion
    }
}
