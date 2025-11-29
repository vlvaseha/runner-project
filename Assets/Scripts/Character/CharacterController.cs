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
        private readonly CharacterStateData _characterStateData;
        
        private IPlayerInput _playerInput;

        public CharacterConfig CharacterConfig { get; }

        public CharacterController(CharacterView characterView, SignalBus signalBus, CharacterConfig characterConfig)
        {
            _characterView = characterView;
            _signalBus = signalBus;
            _characterStateMachine = new CharacterStateMachine();
            _characterStateData = new CharacterStateData(characterView);
            
            CharacterConfig = characterConfig;
        }

        public void Initialize()
        {
            _characterView.OnAnimatorEventReceived += AnimatorEventReceivedHandler;
            
            _signalBus.Subscribe<FlyingPowerUpCollectedSignal>(_characterStateData.OnPowerUpCollected);
            _signalBus.Subscribe<SprintRunningPowerUpCollectedSignal>(_characterStateData.OnPowerUpCollected);
            _signalBus.Subscribe<SlowdownPowerUpCollectedSignal>(_characterStateData.OnPowerUpCollected);

            InitializeStateMachine();
        }

        public void Dispose()
        {
            _characterStateMachine.Dispose();
            _characterStateData.Dispose();
            _characterView.OnAnimatorEventReceived -= AnimatorEventReceivedHandler;
            
            _signalBus.Unsubscribe<FlyingPowerUpCollectedSignal>(_characterStateData.OnPowerUpCollected);
            _signalBus.Unsubscribe<SprintRunningPowerUpCollectedSignal>(_characterStateData.OnPowerUpCollected);
            _signalBus.Unsubscribe<SlowdownPowerUpCollectedSignal>(_characterStateData.OnPowerUpCollected);
        }

        public void OnLevelStarted(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _characterStateData.SetDefaultMovementState();
        }

        public void LogicUpdate()
        {
            _characterStateMachine.UpdateInput(_playerInput.Input);
            _characterStateMachine.LogicUpdate(Time.deltaTime);
        }

        public Vector3 GetViewPosition() => _characterView.ViewRoot.position;

        private void InitializeStateMachine()
        {
            CharacterIdleState characterIdleState = new CharacterIdleState(this, _characterView);
            CharacterMovementState movementState = new CharacterMovementState(this, _characterView);
            CharacterFlyingState flyingState = new CharacterFlyingState(this, _characterView);
            CharacterSlowedRunState characterSlowedRunState = new CharacterSlowedRunState(this, _characterView);
            CharacterSprintRunState characterSprintRunState = new CharacterSprintRunState(this, _characterView);

            Transition<CharacterMovementState> toMovementTransition = new Transition<CharacterMovementState>(movementState, _characterStateData.IsDefaultMovementActive);
            Transition<CharacterMovementState> toFlyingTransition = new Transition<CharacterMovementState>(flyingState, _characterStateData.IsFlyingMovementActive);
            Transition<CharacterMovementState> toCharacterSlowedRunTransition = new Transition<CharacterMovementState>(characterSlowedRunState, _characterStateData.IsSlowdownMovementActive);
            Transition<CharacterMovementState> toCharacterSprintTransition = new Transition<CharacterMovementState>(characterSprintRunState, _characterStateData.IsSprintRunningActive);
            
            characterIdleState.SetTransition(toMovementTransition);
            movementState.SetTransition(toFlyingTransition, toCharacterSlowedRunTransition, toCharacterSprintTransition);
            flyingState.SetTransition(toMovementTransition, toCharacterSlowedRunTransition, toCharacterSprintTransition);
            characterSlowedRunState.SetTransition(toMovementTransition, toFlyingTransition, toCharacterSprintTransition);
            characterSprintRunState.SetTransition(toMovementTransition, toCharacterSlowedRunTransition, toFlyingTransition);
            
            _characterStateMachine.ChangeState(characterIdleState);
        }

        private void AnimatorEventReceivedHandler(string eventName) => _characterStateMachine.AnimatorEventTriggered(eventName);
    }
}
