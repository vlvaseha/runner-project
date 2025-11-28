using System.Collections.Generic;
using PowerUps;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Чанк на уровне, содержит точку в которой можно заспавнить персонажа, а так же поверапы
    /// </summary>
    public class LevelChunk : MonoBehaviour
    {
        private readonly List<BaseCollectablePowerUp> _chunkPowerUps = new List<BaseCollectablePowerUp>();

        [SerializeField] private Transform characterInitialTransform;
        [Space]
        [SerializeField] private int length = 16;
        [SerializeField] private float width = 5.8f;

        public Transform CharacterInitialTransform => characterInitialTransform;
        public int Length => length;
        public float Width => width;

        public void ClearPowerUps()
        {
            _chunkPowerUps.ForEach(b => Destroy(b.gameObject));
            _chunkPowerUps.Clear();
        }

        public void AddBonus(BaseCollectablePowerUp powerUp) => _chunkPowerUps.Add(powerUp);
    }
}
