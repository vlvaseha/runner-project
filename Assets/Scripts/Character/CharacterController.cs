using Character.States;
using Gameplay;
using UnityEngine;

namespace Character
{
    public class CharacterController
    {
        #region Fields

        private readonly CharacterView _characterView;
        private readonly CharacterStateMachine _characterStateMachine;
        
        private IPlayerInput _playerInput;
        
        #endregion

        #region Properties

        public CharacterData Data { get; }
        public float MaxSideMoveOffset { get; private set; }

        #endregion

        #region Class lifecycle

        public CharacterController(CharacterView characterView)
        {
            _characterView = characterView;
            _characterStateMachine = new CharacterStateMachine();
            Data = new CharacterData();
            MaxSideMoveOffset = 2.5f;
        }

        public void Initialize(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _characterStateMachine.Initialize(new CharacterMovementState(this, _characterView, _characterStateMachine));
            _characterView.OnAnimatorEventReceived += AnimatorEventReceivedHandler;
        }

        public void Dispose()
        {
            _characterStateMachine.Dispose();
            _characterView.OnAnimatorEventReceived -= AnimatorEventReceivedHandler;
        }

        #endregion

        #region Methods

        public void LogicUpdate()
        {
            _characterStateMachine.UpdateInput(_playerInput.Input);
            _characterStateMachine.LogicUpdate();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _characterStateMachine.ChangeState(new CharacterSprintRunState(this, _characterView, _characterStateMachine, 18f, 10f));
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _characterStateMachine.ChangeState(new CharacterSlowedRunState(this, _characterView, _characterStateMachine, 2f, 10f));
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _characterStateMachine.ChangeState(new CharacterFlyingState(this, _characterView, _characterStateMachine));
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _characterStateMachine.ChangeState(new CharacterMovementState(this, _characterView, _characterStateMachine));
            }
        }

        public Vector3 GetViewPosition() => _characterView.ViewRoot.position;

        private void AnimatorEventReceivedHandler(string eventName) =>
            _characterStateMachine.AnimatorEventTriggered(eventName);

        #endregion
    }
}
