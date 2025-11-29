using UnityEngine;

namespace PowerUps
{
    [CreateAssetMenu(fileName = nameof(SprintPowerUpConfig), menuName = "Configs/PowerUps/" +  nameof(SprintPowerUpConfig))]
    public class SprintPowerUpConfig : BasePowerUpConfig
    {
        [Space, Header("Movement Settings")]
        public float springRunningSpeed = 12f;
    }
}
