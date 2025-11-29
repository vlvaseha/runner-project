using PowerUps;

namespace Signals.PowerUpSignals
{
    public class SprintRunningPowerUpCollectedSignal : BasePowerUpCollectedSignal
    {
        public SprintRunningPowerUpCollectedSignal(BasePowerUpConfig powerUpConfig) : base(powerUpConfig) { }
        
        public float GetDuration() => PowerUpConfig.duration;
    }
}
