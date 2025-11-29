using UnityEngine;

namespace PowerUps
{
    [CreateAssetMenu(fileName = nameof(FlyingPowerUpConfig),
        menuName = "Configs/PowerUps/" + nameof(FlyingPowerUpConfig))]
    public class FlyingPowerUpConfig : BasePowerUpConfig
    {
        [Space, Header("Movement Settings")] 
        public float flyingHeight = 1f;
        public float jumpDownDuration = 0.15f;
        public float jumpUpDuration = 0.3f;
        public float flyingForwardMoveSpeed = 13;
        public float zRotationMaxAngle = 18f;
        public float zRotationSpeed = 12f;
    }
}

