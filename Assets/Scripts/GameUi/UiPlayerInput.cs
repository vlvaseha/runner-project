using Gameplay;
using UnityEngine;

namespace GameUi
{
    public class UiPlayerInput : MonoBehaviour, IPlayerInput
    {
        #region Fields

        [SerializeField] private VariableJoystick _variableJoystick;

        #endregion

        #region Properties

        public float HorizontalInput { get; private set; }

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _variableJoystick.SetMode(JoystickType.Floating);
            _variableJoystick.AxisOptions = AxisOptions.Horizontal;
        }

        private void Update()
        {
            HorizontalInput = _variableJoystick.Horizontal;
        }

        #endregion
    }
}
