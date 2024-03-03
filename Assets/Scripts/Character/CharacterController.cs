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

        #region Class lifecycle

        public CharacterController(CharacterView characterView)
        {
            _characterView = characterView;
            _characterStateMachine = new CharacterStateMachine();
        }

        public void Initialize(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _characterStateMachine.Initialize(new CharacterMovementState(this, _characterView));
        }

        #endregion

        #region Methods

        public void LogicUpdate()
        {
            _characterStateMachine.UpdateInput(_playerInput.Input);
            _characterStateMachine.LogicUpdate();
        }

        public Vector3 GetViewPosition() => _characterView.ViewRoot.position;

        #endregion
    }
}
