using UnityEngine;

namespace GameUi
{
    public class TapToPlayView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _hideAnimation;

        #endregion

        #region Methods

        public void Hide() => _hideAnimation.gameObject.SetActive(true);

        #endregion
    }
}
