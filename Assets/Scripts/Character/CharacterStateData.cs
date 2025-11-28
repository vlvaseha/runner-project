using System;
using Signals.PowerUpSignals;
using UniRx;

namespace Character
{
    public class CharacterStateData
    {
        private readonly CharacterView _characterView;
        
        private IDisposable _effectDurationHandler;
        private bool _isDefaultMovementActive;
        private bool _isSprintRunningActive;
        private bool _isSlowdownMovementActive;
        private bool _isFlyingMovementActive;

        public CharacterStateData(CharacterView view)
        {
            _characterView = view;
        }

        public void Dispose() =>  _effectDurationHandler?.Dispose();
        
        public bool IsDefaultMovementActive() => _isDefaultMovementActive;
        
        public bool IsSprintRunningActive() => _isSprintRunningActive;
        
        public bool IsSlowdownMovementActive() => _isSlowdownMovementActive;
        
        public bool IsFlyingMovementActive() => _isFlyingMovementActive;

        public void SetDefaultMovementState()
        {
            ResetState();
            _isDefaultMovementActive = true;
        }
        
        public void OnPowerUpCollected(BasePowerUpCollectedSignal powerUpCollectedSignal)
        {
            ResetState();
            _effectDurationHandler?.Dispose();
            
            switch (powerUpCollectedSignal)
            {
                case FlyingPowerUpCollectedSignal flyingPowerUpCollectedSignal:
                    _isFlyingMovementActive = true;
                    _effectDurationHandler = Observable
                        .Timer(TimeSpan.FromSeconds(flyingPowerUpCollectedSignal.PowerUpDuration))
                        .Subscribe(_ => ResetToDefaultMovementState())
                        .AddTo(_characterView);
                    
                    break;
                case SprintRunningPowerUpCollectedSignal sprintRunningPowerUpCollectedSignal:
                    _isSprintRunningActive = true;
                    _effectDurationHandler = Observable
                        .Timer(TimeSpan.FromSeconds(sprintRunningPowerUpCollectedSignal.PowerUpDuration))
                        .Subscribe(_ => ResetToDefaultMovementState())
                        .AddTo(_characterView);
                    
                    break;
                case SlowdownPowerUpCollectedSignal slowdownPowerUpCollectedSignal:
                    _isSlowdownMovementActive = true;
                    _effectDurationHandler = Observable
                        .Timer(TimeSpan.FromSeconds(slowdownPowerUpCollectedSignal.PowerUpDuration))
                        .Subscribe(_ => ResetToDefaultMovementState())
                        .AddTo(_characterView);
                    
                    break;
            }
        }

        private void ResetToDefaultMovementState()
        {
            ResetState();
            _isDefaultMovementActive = true;
        }

        private void ResetState()
        {
            _isDefaultMovementActive = false;
            _isSprintRunningActive = false;
            _isSlowdownMovementActive = false;
            _isFlyingMovementActive = false;
        }
    }
}
