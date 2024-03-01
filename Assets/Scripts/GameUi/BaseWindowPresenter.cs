namespace GameUi
{
    public abstract class BaseWindowPresenter<TWindow> : WindowArguments where TWindow : Window
    {
        #region Fields

        protected TWindow WindowView { get; private set; }
        
        #endregion

        #region Properies

        public bool IsViewInitialized => WindowView != null;
        
        #endregion

        #region Class lifecycle

        protected BaseWindowPresenter(WindowType type) : base(type) { }
        
        #endregion

        #region Methods

        public virtual void Initialize(TWindow windowView)
        {
            WindowView = windowView;
        }
        
        #endregion
    }
}