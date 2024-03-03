using System;
using UnityEngine;

namespace Character.States
{
    public class BaseCharacterState
    {
        #region Properties

        protected CharacterController CharacterController { get; }
        protected CharacterView CharacterView { get; }
        protected CharacterStateMachine CharacterStateMachine { get; }
        
        #endregion

        #region Class lifecycle

        protected BaseCharacterState(CharacterController characterController, CharacterView characterView, 
            CharacterStateMachine characterStateMachine)
        {
            CharacterController = characterController;
            CharacterView = characterView;
            CharacterStateMachine = characterStateMachine;
        }
        
        #endregion

        #region Methods

        public virtual void Enter() { }

        public virtual void Exit(Action onComplete = null) { onComplete?.Invoke();}

        public virtual void UpdateInput(Vector2 input) { }

        public virtual void LogicUpdate() { }

        public virtual void AnimatorEventTriggered(string eventName) { }

        #endregion
    }
}
