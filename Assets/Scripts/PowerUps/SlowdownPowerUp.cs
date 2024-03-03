using PowerUps;
using Signals.PowerUpSignals;

namespace CollectableBonus
{
    public class SlowdownPowerUp : BaseCollectablePowerUp
    {
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();

            SignalBus.Fire(new SlowdownPowerUpCollectedSignal(2f, PowerUpDuration));
        }
    }
}
