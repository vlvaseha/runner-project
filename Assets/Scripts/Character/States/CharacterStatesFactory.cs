using System;
using PowerUps;
using Zenject;

namespace Character.States
{
    public class CharacterStatesFactory
    {
        private readonly DiContainer _diContainer;
        private readonly CharacterMovementSettings _characterMovementSettings;
        private readonly CharacterView _characterView;

        public CharacterStatesFactory(DiContainer diContainer, CharacterView characterView)
        {
            _diContainer = diContainer;
            _characterView = characterView;
            _characterMovementSettings = _diContainer.Resolve<CharacterConfig>().characterMovementSettings;
        }

        public T CreateState<T>() where T : BaseState
        {
            switch (typeof(T).Name)
            {
                case nameof(CharacterMovementState):
                    return new CharacterMovementState(_characterView, _characterMovementSettings) as T;
                
                case nameof(CharacterIdleState):
                    return new CharacterIdleState(_characterView) as T;
                
                case nameof(CharacterFlyingState):
                    BasePowerUpConfig flyingPowerUpConfig =
                        _diContainer.ResolveId<BasePowerUpConfig>(CollectablePowerUpId.FlyingPowerUp);
                    return new CharacterFlyingState(_characterView, _characterMovementSettings, flyingPowerUpConfig) as T;
                
                case nameof(CharacterSprintRunState):
                    BasePowerUpConfig sprintPowerUpConfig =
                        _diContainer.ResolveId<BasePowerUpConfig>(CollectablePowerUpId.SprintRunningPowerUp);
                    return new CharacterSprintRunState(_characterView, _characterMovementSettings, sprintPowerUpConfig) as T;
                
                case nameof(CharacterSlowedRunState):
                    BasePowerUpConfig slowedRunPowerUpConfig =
                        _diContainer.ResolveId<BasePowerUpConfig>(CollectablePowerUpId.SlowdownPowerUp);
                    return new CharacterSlowedRunState(_characterView, _characterMovementSettings, slowedRunPowerUpConfig) as T;
                
                default:
                    throw new ArgumentException($"Unknown CharactersState type: {typeof(T).Name}");
            }
        }
    }
}

