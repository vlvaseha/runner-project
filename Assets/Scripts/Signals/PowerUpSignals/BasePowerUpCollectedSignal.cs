namespace Signals.PowerUpSignals
{
    public class BasePowerUpCollectedSignal 
    {
        #region Properties

        public float PowerUpDuration { get; }

        #endregion

        #region Class lifecycle

        public BasePowerUpCollectedSignal(float duration)
        {
            PowerUpDuration = duration;
        }

        #endregion
    }
}
