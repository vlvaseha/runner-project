using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/" + nameof(CharacterConfig))]
    public class CharacterConfig : ScriptableObject
    {
        public CharacterMovementSettings characterMovementSettings;
    }
}

