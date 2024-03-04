using Signals.PowerUpSignals;

namespace PowerUps
{
    /// <summary>
    /// Класс бонуса при котором персонаж замедляется, отправляет нужный сигнал
    /// </summary>
    public class SlowdownPowerUp : BaseCollectablePowerUp
    {
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();
            SignalBus.Fire(new SlowdownPowerUpCollectedSignal(2f, PowerUpDuration));
        }
    }
}
