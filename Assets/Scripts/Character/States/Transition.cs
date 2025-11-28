using System;

namespace Character.States
{
    public abstract class BaseTransition
    {
        public abstract bool Evaluate();
        
        public abstract void Apply(CharacterStateMachine machine);
    }
    
    public class Transition<TState> : BaseTransition where TState : BaseCharacterState<TState> 
    {
        private readonly Func<bool> _condition;
        private readonly TState _target;

        public Transition(TState target, Func<bool> condition)
        {
            _target = target;
            _condition = condition;
        }
    
        public override bool Evaluate() => _condition();

        public override void Apply(CharacterStateMachine machine) => machine.ChangeState(_target);
    }
}

