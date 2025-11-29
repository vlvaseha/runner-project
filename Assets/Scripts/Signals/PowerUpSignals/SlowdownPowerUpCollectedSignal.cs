using PowerUps;

namespace Signals.PowerUpSignals
{
    public class SlowdownPowerUpCollectedSignal : BasePowerUpCollectedSignal
    {
        public SlowdownPowerUpCollectedSignal(BasePowerUpConfig powerUpConfig) : base(powerUpConfig) { }
        
        public float GetDuration() => PowerUpConfig.duration;
    }
}
