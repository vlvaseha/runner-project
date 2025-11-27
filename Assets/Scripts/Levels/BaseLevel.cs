namespace Levels
{
    public abstract class BaseLevel
    {
        public abstract void Initialize();

        public abstract void StartLevel();
        
        public abstract void CompleteLevel();
        
        public abstract void FailLevel();
        
        public abstract void DestroyLevel();
    }
}
