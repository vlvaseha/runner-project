using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Айдл состояние персонажа
    /// </summary>
    public class CharacterIdleState : BaseCharacterState
    {
        #region Fields

        private readonly int IdleStateHash = Animator.StringToHash("Idle");

        #endregion
        
        #region Class lifecycle

        public CharacterIdleState(CharacterController characterController, CharacterView characterView,
            CharacterStateMachine characterStateMachine) : base(characterController, characterView, characterStateMachine) { }
        
        #endregion

        #region Methods

        public override void Enter()
        {
            CharacterView.Animator.Play(IdleStateHash);
        }

        #endregion
    }
}
