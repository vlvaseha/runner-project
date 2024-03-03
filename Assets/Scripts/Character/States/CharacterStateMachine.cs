using UnityEngine;

namespace Character.States
{
    public class CharacterStateMachine
    {
        #region Fields

        private BaseCharacterState _currentState;
        
        #endregion

        #region Methods

        public void Initialize(BaseCharacterState startState)
        {
            _currentState = startState;
            _currentState.Enter();
        }

        public void ChangeState(BaseCharacterState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void UpdateInput(Vector2 input) => _currentState.UpdateInput(input);
        
        public void LogicUpdate() => _currentState.LogicUpdate();

        #endregion
    }
}
