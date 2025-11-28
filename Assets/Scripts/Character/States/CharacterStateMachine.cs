using UnityEngine;

namespace Character.States
{
    /// <summary>
    /// Класс осузествляющий управление состояниями персонажа
    /// </summary>
    public class CharacterStateMachine
    {
        private BaseState _currentState;

        public void Dispose()
        {
            _currentState?.Exit();
        }

        public void ChangeState(BaseState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void UpdateInput(Vector2 input) => _currentState?.UpdateInput(input);
        
        public void LogicUpdate(float dt)
        {
            if (_currentState is null)
            {
                return;
            }
        
            BaseTransition transition = _currentState.GetTransition();

            if (transition is not null)
            {
                transition.Apply(this);
                return;
            }
        
            _currentState.Tick(dt);
        }

        public void AnimatorEventTriggered(string eventName) => _currentState?.AnimatorEventTriggered(eventName);
    }
}
