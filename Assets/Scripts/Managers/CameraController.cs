using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Managers
{
    /// <summary>
    /// Класс камеры, содержит логику передвижения за таргетом
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _followSmoothTime = 3f;

        private Sequence _movementSequence;
        private Vector3 _velocity;
        private Vector3 _baseOffset;

        #endregion

        #region Methods
        
        public void CalculateCameraOffset(Vector3 targetPosition)
        {
            _baseOffset = targetPosition - transform.position;
            _baseOffset.x = 0f;
        }

        public void UpdatePosition(Vector3 targetPosition)
        {
            Vector3 currentPosition = transform.position;
            Vector3 offset = targetPosition - currentPosition;
            offset.x = 0f;
            
            Vector3 offsetPos = offset - _baseOffset;
            Vector3 newPos = currentPosition + offsetPos;
            
            transform.position = Vector3.SmoothDamp(currentPosition, newPos, ref _velocity, _followSmoothTime);
        }

        #endregion
    }
}

