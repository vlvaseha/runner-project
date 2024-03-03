namespace Signals.PowerUpSignals
{
    public class SlowdownPowerUpCollectedSignal : BasePowerUpCollectedSignal
    {
        public float SlowdownSpeed { get; }

        public SlowdownPowerUpCollectedSignal(float slowdownSpeed, float duration) : base(duration)
        {
            SlowdownSpeed = slowdownSpeed;
        }
    }
}
