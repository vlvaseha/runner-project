using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUi
{
    public class UiStartPanel : MonoBehaviour
    {
        public event Action OnStartGameButtonClicked;
        
        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(StartButtonClickedHandler);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(StartButtonClickedHandler);
        }

        private void StartButtonClickedHandler() => OnStartGameButtonClicked?.Invoke();
    }
}
