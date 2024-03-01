using UnityEngine;

namespace Levels
{
    public class LevelChunk : MonoBehaviour
    {
        #region Fields

        [SerializeField] private int _length = 16;
        [SerializeField] private float _width = 5.8f;

        #endregion

        #region Properties

        public int Length => _length;
        public float Width => _width;

        #endregion
    }
}
