using Signals.PowerUpSignals;
using Zenject;

namespace PowerUps
{
    /// <summary>
    /// Класс бонуса при котором персонаж ускоряется, отправляет нужный сигнал
    /// </summary>
    public class SprintRunningPowerUp : BaseCollectablePowerUp
    {
        [Inject]
        private void Construct(SignalBus signalBus, 
            [Inject(Id = CollectablePowerUpId.SprintRunningPowerUp)] BasePowerUpConfig powerUpConfig)
        {
            SignalBus = signalBus;
            PowerUpConfig = powerUpConfig;
        }
        
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();
            SignalBus.Fire(new SprintRunningPowerUpCollectedSignal(PowerUpConfig));
        }
    }
}
