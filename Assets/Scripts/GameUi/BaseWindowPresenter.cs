using System;

namespace GameUi
{
    public abstract class BaseWindowPresenter<TWindow> : WindowArguments where TWindow : Window
    {
        public event Action OnInitialized;
        
        protected TWindow WindowView { get; private set; }

        public bool IsViewInitialized => WindowView is not null;

        public void SetView(TWindow windowView)
        {
            WindowView = windowView;
            OnInitialized?.Invoke();
        }

        public void CloseWindow()
        {
            if (IsViewInitialized)
            {
                WindowView.CloseWindow();
            }
        }
    }
}