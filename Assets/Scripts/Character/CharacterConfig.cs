using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/" + nameof(CharacterConfig))]
    public class CharacterConfig : ScriptableObject
    {
        public float maxSideMovementOffset = 2.5f;
        public float sideMoveSpeed = 18f;
        public float yRotationSpeed = 90;
        public float yRotationMaxAngle = 12;
        public float zRotationSpeed = 18f;
        public float zRotationMaxAngle = 12;
        
        [Header("Default running state")]
        public float runningAnimationMultiplier = 1.4f;
        public float forwardMoveSpeed = 8f;
        
        [Header("Flying state")] 
        public float flyingForwardMoveSpeed = 13f;
        public float flyingHeight = 1f;
        public float jumpUpDuration = .3f;
        public float jumpDownDuration = .15f;
        
        [Header("Slowed running state")]
        public float runningAnimationSpeed = .5f;
        public float slowRunningSpeed = 3f;

        [Header("Sprint state")] 
        public float springRunningSpeed = 12f;
    }
}

