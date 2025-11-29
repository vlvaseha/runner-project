using UnityEngine;

namespace PowerUps
{
    public class BasePowerUpConfig : ScriptableObject
    {
        public string id = CollectablePowerUpId.FlyingPowerUp; 
        public float duration = 10f;
    }
}

