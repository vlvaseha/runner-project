using Signals.PowerUpSignals;

namespace PowerUps
{
    public class FlyingPowerUp : BaseCollectablePowerUp
    {
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();
            
            SignalBus.Fire(new FlyingPowerUpCollectedSignal(PowerUpDuration));
        }
    }
}
