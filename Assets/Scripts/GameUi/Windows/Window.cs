using UnityEngine;

namespace GameUi
{
    public abstract class Window : MonoBehaviour
    {
        public WindowAnimation OpenWindowAnimation { get; protected set; }
        public WindowAnimation CloseWindowAnimation { get; protected set; }
        
        protected WindowArguments Arguments { get; private set; }
        protected UiWindows UiWindows { get; private set; }

        public void Initialize(WindowArguments arguments, UiWindows uiWindowsController)
        {
            Arguments = arguments;
            UiWindows = uiWindowsController;
            
            UiWindows.OnWindowCloseCompleted += WindowCloseCompletedHandler;
            UiWindows.OnWindowOpenCompleted += WindowOpenCompletedHandler;
            UiWindows.OnWindowCloseStarted += WindowCloseStartedHandler;
            UiWindows.OnWindowOpenStarted += WindowOpenStartedHandler;
        }

        public void Dispose()
        {
            UiWindows.OnWindowCloseCompleted -= WindowCloseCompletedHandler;
            UiWindows.OnWindowOpenCompleted -= WindowOpenCompletedHandler;
            UiWindows.OnWindowCloseStarted -= WindowCloseStartedHandler;
            UiWindows.OnWindowOpenStarted -= WindowOpenStartedHandler;
        }
        
        protected virtual void OnOpenStarted() { }
        
        protected virtual void OnOpenCompleted() { }
        
        protected virtual void OnCloseStarted() { }

        protected virtual void OnCloseCompleted() { }
        
        private void WindowOpenStartedHandler(Window window)
        {
            if (ReferenceEquals(window, this))
            {
                OnOpenStarted();
            }
        }

        private void WindowOpenCompletedHandler(Window window)
        {
            if (ReferenceEquals(window, this))
            {
                OnOpenCompleted();
            }
        }

        private void WindowCloseStartedHandler(Window window)
        {
            if (ReferenceEquals(window, this))
            {
                OnCloseStarted();
            }
        }

        private void WindowCloseCompletedHandler(Window window)
        {
            if (ReferenceEquals(window, this))
            {
                OnCloseCompleted();
            }
        }
    }
}
