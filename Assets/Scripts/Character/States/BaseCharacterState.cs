using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Базовый класс для состояний персонажа
    /// </summary>
    public class BaseCharacterState<TState> : BaseState where TState : BaseCharacterState<TState>
    {
        protected CharacterView CharacterView { get; }

        protected BaseCharacterState(CharacterView characterView)
        {
            CharacterView = characterView;
        }

        public override void Enter() => ((TState)this).OnEnter();

        public override void Exit() => ((TState)this).OnExit();

        public override void UpdateInput(Vector2 input) => OnInputUpdated(input);

        public override void Tick(float dt) => ((TState)this).OnTick(dt);

        public override void AnimatorEventTriggered(string eventName) => ((TState)this).OnAnimatorEventTriggered(eventName);
        
        protected virtual void OnEnter() { }
        
        protected virtual void OnExit() { }
        
        protected virtual void OnTick(float dt) { }
        
        protected virtual void OnAnimatorEventTriggered(string eventName) { }
        
        protected virtual void OnInputUpdated(Vector2 input) { }
    }
}
