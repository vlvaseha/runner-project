using Signals.PowerUpSignals;

namespace PowerUps
{
    /// <summary>
    /// Класс бонуса при котором персонаж ускоряется, отправляет нужный сигнал
    /// </summary>
    public class SprintRunningPowerUp : BaseCollectablePowerUp
    {
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();
            SignalBus.Fire(new SprintRunningPowerUpCollectedSignal(18f, PowerUpDuration));
        }
    }
}
