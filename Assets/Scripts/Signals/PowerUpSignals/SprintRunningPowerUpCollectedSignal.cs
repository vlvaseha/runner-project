namespace Signals.PowerUpSignals
{
    public class SprintRunningPowerUpCollectedSignal : BasePowerUpCollectedSignal
    {
        public float RunningSpeed { get; }

        public SprintRunningPowerUpCollectedSignal(float runningSpeed, float duration) : base(duration)
        {
            RunningSpeed = runningSpeed;
        }
    }
}
