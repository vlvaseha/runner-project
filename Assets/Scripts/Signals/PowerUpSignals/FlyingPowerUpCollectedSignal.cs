using PowerUps;

namespace Signals.PowerUpSignals
{
    public class FlyingPowerUpCollectedSignal : BasePowerUpCollectedSignal
    {
        public FlyingPowerUpCollectedSignal(BasePowerUpConfig powerUpConfig) : base(powerUpConfig) { }
        
        public float GetDuration() => PowerUpConfig.duration;
    }
}
