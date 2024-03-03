using System.Collections.Generic;
using UnityEngine;

namespace Character.States
{
    public class CharacterStateMachine
    {
        #region Fields

        private readonly Queue<BaseCharacterState> _statesQueue = new Queue<BaseCharacterState>();
        private BaseCharacterState _currentState;
        
        #endregion

        #region Class lifecycle

        public void Initialize(BaseCharacterState startState)
        {
            _currentState = startState;
            _currentState.Enter();
        }

        public void Dispose()
        {
            _currentState?.Exit();
        }

        #endregion

        #region Methods

        public void ChangeState(BaseCharacterState newState)
        {
            if (_statesQueue.Count == 0)
            {
                _statesQueue.Enqueue(newState);
                _currentState.Exit(() =>
                {
                    _currentState = _statesQueue.Dequeue();
                    _currentState.Enter();
                });
            }
            else
            {
                _statesQueue.Dequeue();
                _statesQueue.Enqueue(newState);
            }
        }

        public void UpdateInput(Vector2 input) => _currentState.UpdateInput(input);
        
        public void LogicUpdate() => _currentState.LogicUpdate();

        public void AnimatorEventTriggered(string eventName) => _currentState.AnimatorEventTriggered(eventName);

        #endregion
    }
}
