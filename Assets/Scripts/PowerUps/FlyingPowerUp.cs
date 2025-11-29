using Signals.PowerUpSignals;
using Zenject;

namespace PowerUps
{
    /// <summary>
    /// Класс бонуса при котором персонаж летит, отправляет нужный сигнал
    /// </summary>
    public class FlyingPowerUp : BaseCollectablePowerUp
    {
        [Inject] 
        private void Construct(SignalBus signalBus, [Inject(Id = CollectablePowerUpId.FlyingPowerUp)] BasePowerUpConfig config)
        {
            SignalBus = signalBus;
            PowerUpConfig = config;
        }
        
        protected override void PlayerTriggeredHandler()
        {
            base.PlayerTriggeredHandler();
            SignalBus.Fire(new FlyingPowerUpCollectedSignal(PowerUpConfig));
        }
    }
}
