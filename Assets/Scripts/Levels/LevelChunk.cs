using System.Collections.Generic;
using CollectableBonus;
using UnityEngine;

namespace Levels
{
    public class LevelChunk : MonoBehaviour
    {
        #region Fields
        
        private readonly List<BaseCollectableBonus> _chunkBonuses = new List<BaseCollectableBonus>();

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

        public void ClearBonuses()
        {
            _chunkBonuses.ForEach(b => Destroy(b.gameObject));
            _chunkBonuses.Clear();
        }

        public void AddBonus(BaseCollectableBonus bonus) => _chunkBonuses.Add(bonus);

        #endregion
    }
}
