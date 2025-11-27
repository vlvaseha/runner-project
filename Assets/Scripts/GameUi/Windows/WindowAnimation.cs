using System;

namespace GameUi
{
    public abstract class WindowAnimation
    {
        public abstract void Animate(Window window, Action<Window> onAnimationComplete);
    }
}