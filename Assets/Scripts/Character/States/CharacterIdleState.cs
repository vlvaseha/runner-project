using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Айдл состояние персонажа
    /// </summary>
    public class CharacterIdleState : BaseCharacterState<CharacterIdleState>
    {
        private static readonly int IdleStateHash = Animator.StringToHash("Idle");
        
        public CharacterIdleState(CharacterView characterView) : base(characterView) { }

        public override void Enter()
        {
            CharacterView.Animator.Play(IdleStateHash);
        }
    }
}
