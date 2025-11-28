using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Айдл состояние персонажа
    /// </summary>
    public class CharacterIdleState : BaseCharacterState<CharacterIdleState>
    {
        private readonly int IdleStateHash = Animator.StringToHash("Idle");
        
        public CharacterIdleState(CharacterController characterController, CharacterView characterView)
            : base(characterController, characterView) { }

        public override void Enter()
        {
            CharacterView.Animator.Play(IdleStateHash);
        }
    }
}
