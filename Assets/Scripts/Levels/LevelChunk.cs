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
        #region Fields
        
        private readonly List<BaseCollectablePowerUp> _chunkPowerUps = new List<BaseCollectablePowerUp>();

        [SerializeField] private Transform _characterInitialTransform;
        [Space]
        [SerializeField] private int _length = 16;
        [SerializeField] private float _width = 5.8f;

        #endregion

        #region Properties

        public Transform CharacterInitialTransform => _characterInitialTransform;
        public int Length => _length;
        public float Width => _width;

        #endregion

        #region Methods

        public void ClearPowerUps()
        {
            _chunkPowerUps.ForEach(b => Destroy(b.gameObject));
            _chunkPowerUps.Clear();
        }

        public void AddBonus(BaseCollectablePowerUp powerUp) => _chunkPowerUps.Add(powerUp);

        #endregion
    }
}
