using UnityEngine;

namespace PowerUps
{
    [CreateAssetMenu(fileName = nameof(SlowdownPowerUp), menuName = "Configs/PowerUps/" + nameof(SlowdownPowerUp))]
    public class SlowDownPowerUpConfig : BasePowerUpConfig
    {
        [Space, Header("Movement Settings")]
        public float runningAnimationSpeed = 0.5f;
        public float slowRunningSpeed = 3f;
    }
}

