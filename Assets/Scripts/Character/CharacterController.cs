using Character.States;
using Gameplay;
using Signals.PowerUpSignals;
using UnityEngine;
using Zenject;

namespace Character
{
    /// <summary>
    /// Основной класс через который персонаж взаимодействует с игрой, получает инпут ивенты и тд
    /// </summary>
    public class CharacterController
    {
        private readonly CharacterView _characterView;
        private readonly CharacterStateMachine _characterStateMachine;
        private readonly SignalBus _signalBus;
        
        private IPlayerInput _playerInput;

        public CharacterData Data { get; }
        public float MaxSideMoveOffset { get; private set; }

        public CharacterController(CharacterView characterView, SignalBus signalBus)
        {
            _characterView = characterView;
            _signalBus = signalBus;
            _characterStateMachine = new CharacterStateMachine();
            
            Data = new CharacterData();
            MaxSideMoveOffset = 2.5f;
        }

        public void Initialize(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _characterStateMachine.Initialize(new CharacterMovementState(this, _characterView, _characterStateMachine));
            _characterView.OnAnimatorEventReceived += AnimatorEventReceivedHandler;
            
            _signalBus.Subscribe<FlyingPowerUpCollectedSignal>(FlyingPowerUpCollected);
            _signalBus.Subscribe<SprintRunningPowerUpCollectedSignal>(SprintRunningPowerUpCollected);
            _signalBus.Subscribe<SlowdownPowerUpCollectedSignal>(SlowDownPowerUpCollected);
        }

        public void Dispose()
        {
            _characterStateMachine.Dispose();
            _characterView.OnAnimatorEventReceived -= AnimatorEventReceivedHandler;
            
            _signalBus.Unsubscribe<FlyingPowerUpCollectedSignal>(FlyingPowerUpCollected);
            _signalBus.Unsubscribe<SprintRunningPowerUpCollectedSignal>(SprintRunningPowerUpCollected);
            _signalBus.Unsubscribe<SlowdownPowerUpCollectedSignal>(SlowDownPowerUpCollected);
        }

        public void LogicUpdate()
        {
            _characterStateMachine.UpdateInput(_playerInput.Input);
            _characterStateMachine.LogicUpdate();
        }

        public Vector3 GetViewPosition() => _characterView.ViewRoot.position;

        private void AnimatorEventReceivedHandler(string eventName) =>
            _characterStateMachine.AnimatorEventTriggered(eventName);

        private void FlyingPowerUpCollected(FlyingPowerUpCollectedSignal args) =>
            _characterStateMachine.ChangeState(new CharacterFlyingState(this, _characterView, _characterStateMachine, args.PowerUpDuration));

        
        private void SprintRunningPowerUpCollected(SprintRunningPowerUpCollectedSignal args) => 
            _characterStateMachine.ChangeState(new CharacterSprintRunState(this, _characterView, _characterStateMachine, args.RunningSpeed, args.PowerUpDuration));
        
        private void SlowDownPowerUpCollected(SlowdownPowerUpCollectedSignal args) => 
            _characterStateMachine.ChangeState(new CharacterSlowedRunState(this, _characterView, _characterStateMachine, args.SlowdownSpeed, args.PowerUpDuration));
    }
}
