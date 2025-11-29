using System;

namespace Character
{
    [Serializable]
    public class CharacterMovementSettings
    {
        public float maxSideMovementOffset = 2.5f;
        public float sideMoveSpeed = 18f;
        public float yRotationMaxAngle = 12f;
        public float yRotationSpeed = 90;
        public float forwardMoveSpeed = 8f;
        public float runningAnimationMultiplier = 1.4f;
    }

}

