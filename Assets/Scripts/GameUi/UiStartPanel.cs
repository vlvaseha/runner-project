using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameUi
{
    public class UiStartPanel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Button _button;
        
        #endregion

        #region Properties

        public UnityEvent StartButtonClicked => _button.onClick;
        
        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            StartButtonClicked.AddListener(() => gameObject.SetActive(false));
        }

        #endregion
    }
}
