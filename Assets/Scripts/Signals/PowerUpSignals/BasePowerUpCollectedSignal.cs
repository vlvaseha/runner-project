using PowerUps;

namespace Signals.PowerUpSignals
{
    public class BasePowerUpCollectedSignal 
    {
        public BasePowerUpConfig PowerUpConfig { get; }

        protected BasePowerUpCollectedSignal(BasePowerUpConfig powerUpConfig)
        {
            PowerUpConfig = powerUpConfig;
        }
    }
}
