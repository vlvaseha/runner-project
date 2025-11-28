using System.Collections.Generic;
using UnityEngine;

namespace Character.States
{
    public abstract class BaseState
    {
        private readonly List<BaseTransition> _transitions = new List<BaseTransition>();

        public BaseTransition GetTransition()
        {
            foreach (var t in _transitions)
            {
                if (t.Evaluate())
                {
                    return t;
                }
            }

            return null;
        }

        public void SetTransition<TState>(params Transition<TState> [] t) where TState : BaseCharacterState<TState>
        {
            _transitions.AddRange(t);
        }
        
        public abstract void Enter();
        
        public abstract void Exit();

        public abstract void UpdateInput(Vector2 input);
        
        public abstract void Tick(float dt);

        public abstract void AnimatorEventTriggered(string eventName);
    }
}

