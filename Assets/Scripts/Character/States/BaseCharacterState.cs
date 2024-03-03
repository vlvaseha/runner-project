using UnityEngine;

namespace Character.States
{
    public class BaseCharacterState
    {
        #region Properties

        protected CharacterController CharacterController { get; }
        protected CharacterView CharacterView { get; }
        
        #endregion

        #region Class lifecycle

        public BaseCharacterState(CharacterController characterController, CharacterView characterView)
        {
            CharacterController = characterController;
            CharacterView = characterView;
        }
        
        #endregion

        #region Methods

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void UpdateInput(Vector2 input) { }

        public virtual void LogicUpdate() { }
        
        #endregion
    }
}
