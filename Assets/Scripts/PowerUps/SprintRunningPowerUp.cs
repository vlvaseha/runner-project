using PowerUps;
using Signals.PowerUpSignals;

namespace CollectableBonus
{
    public class SprintRunningPowerUp : BaseCollectablePowerUp
    {
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();

            SignalBus.Fire(new SprintRunningPowerUpCollectedSignal(18f, PowerUpDuration));
        }
    }
}
