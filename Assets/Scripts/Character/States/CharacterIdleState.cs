using UnityEngine;

namespace Character.States
{
    public class CharacterIdleState : BaseCharacterState
    {
        #region Fields

        private readonly int IdleStateHash = Animator.StringToHash("Idle");

        #endregion
        
        #region Class lifecycle

        public CharacterIdleState(CharacterController characterController, CharacterView characterView) 
            : base(characterController, characterView) { }
        
        #endregion

        #region Methods

        public override void Enter()
        {
            CharacterView.Animator.Play(IdleStateHash);
        }

        #endregion
    }
}
