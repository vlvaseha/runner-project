using Signals.PowerUpSignals;

namespace PowerUps
{
    /// <summary>
    /// Класс бонуса при котором персонаж летит, отправляет нужный сигнал
    /// </summary>
    public class FlyingPowerUp : BaseCollectablePowerUp
    {
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();
            SignalBus.Fire(new FlyingPowerUpCollectedSignal(PowerUpDuration));
        }
    }
}
