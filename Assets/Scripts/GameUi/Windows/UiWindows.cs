using System;
using System.Collections.Generic;
using Managers;
using Managers.Storage;
using UnityEngine;
using Zenject;

namespace GameUi
{
    public class UiWindows : MonoBehaviour
    {
        public event Action<Window> OnWindowOpenStarted;
        public event Action<Window> OnWindowOpenCompleted;
        public event Action<Window> OnWindowCloseStarted;
        public event Action<Window> OnWindowCloseCompleted;
        
        private readonly LinkedList<WindowOpenRequest> _openWindowQueue = new LinkedList<WindowOpenRequest>();
        private readonly List<Window> _openedWindows = new List<Window>();
        
        [SerializeField] private Transform windowsRoot;
        [SerializeField] private GameObject blockInputGameObject;
        
        private WindowFactory _windowFactory;
        private int _lockInputCounter;

        public bool IsInputLocked => _lockInputCounter > 0;

        public void Initialize(AssetInstanceCreator assetInstanceCreator, PrefabsManager prefabsManager,
            DiContainer diContainer)
        {
            _windowFactory = new WindowAssetFactory(diContainer, prefabsManager, assetInstanceCreator);
        }

        private void Update()
        {
            if (_openedWindows.Count > 0)
            {
                return;
            }

            LinkedListNode<WindowOpenRequest> topWindowQueue = _openWindowQueue.First;

            if (topWindowQueue is null)
            {
                return;
            }
            
            WindowOpenRequest topWindowOpenRequest = topWindowQueue.Value;
            
            if (topWindowOpenRequest is null)
            {
                return;
            }

            _openWindowQueue.Remove(topWindowQueue);
            DoOpenWindow(topWindowOpenRequest);
        }

        public void Open<T>(WindowArguments arguments = null) where T : Window => Open(typeof(T), arguments);
        
        public void CloseWindow(Window window)
        {
            if (_openedWindows.Contains(window) == false)
            {
                Debug.LogWarning("Warning! Tried to close not opened or unknown window!");
                return;
            }

            ProcessWindowCloseStarted(window);
            LockInput();
            
            if (window.CloseWindowAnimation is null)
            {
                HandleWindowClose(window);
            }
            else
            {
                window.CloseWindowAnimation.Animate(window, HandleWindowClose);
            }
        }
        
        protected virtual void ProcessWindowOpenStarted(Window window) => OnWindowOpenStarted?.Invoke(window);
        
        protected virtual void ProcessWindowOpenCompleted(Window window) => OnWindowOpenCompleted?.Invoke(window);
        
        protected virtual void ProcessWindowCloseStarted(Window window) => OnWindowCloseStarted?.Invoke(window);
        
        protected virtual void ProcessWindowCloseCompleted(Window window) => OnWindowCloseCompleted?.Invoke(window);

        private void Open(Type windowType, WindowArguments arguments = null)
        {
            WindowOpenRequest windowOpenRequest = new WindowOpenRequest(windowType, arguments);
            _openWindowQueue.AddLast(windowOpenRequest);
        }
        
        private void DoOpenWindow(WindowOpenRequest topWindowOpenRequest)
        {
            Window window = GetWindow(topWindowOpenRequest.WindowType);
            
            if (window is null)
            {
                Debug.LogWarning("Warning! Can't open window for request: " + topWindowOpenRequest);
                return;
            }

            window.Initialize(topWindowOpenRequest.Arguments, this);
            window.transform.SetAsLastSibling();

            _openedWindows.Add(window);
            ProcessWindowOpenStarted(window);

            if (window.OpenWindowAnimation is null)
            {
                ProcessWindowOpenCompleted(window);
            }
            else
            {
                LockInput();
                window.OpenWindowAnimation.Animate(window, _ =>
                {
                    UnlockInput();
                    ProcessWindowOpenCompleted(window);
                });
            }
        }

        private Window GetWindow(Type windowType) => _windowFactory.Create(windowType, windowsRoot);

        private void HandleWindowClose(Window window)
        {
            UnlockInput();
            _openedWindows.Remove(window);
            
            ProcessWindowCloseCompleted(window);
            window.Dispose();
            Destroy(window.gameObject);
        }

        private void LockInput()
        {
            _lockInputCounter++;
            blockInputGameObject.SetActive(IsInputLocked);
        }

        private void UnlockInput()
        {
            if (_lockInputCounter == 0)
            {
                Debug.LogWarning("Warning! Trying to unlock not locked input!");
                return;
            }

            _lockInputCounter--;
            blockInputGameObject.SetActive(IsInputLocked);
        }

    }
}
