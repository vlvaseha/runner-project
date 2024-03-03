using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUi
{
    public class UiPlayerInput : MonoBehaviour, IPlayerInput, IDragHandler, IEndDragHandler
    {
        #region Fields

        private const float DragTreshold = 2f;
        private const float MaxInputDelta = 50f;

        #endregion

        #region Properties

        public Vector2 Input { get; private set; }

        #endregion

        #region Methods

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.delta.magnitude < DragTreshold)
            {
                Input = Vector2.zero;
            }
            else
            {
                float clampedHorizontalInput = Mathf.Clamp(eventData.delta.x, -MaxInputDelta, MaxInputDelta);
                float clampedVerticalInput = Mathf.Clamp(eventData.delta.y, -MaxInputDelta, MaxInputDelta);
                
                Input = new Vector2(clampedHorizontalInput, clampedVerticalInput) / MaxInputDelta;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Input = Vector2.zero;
        }
        
        #endregion
    }
}
