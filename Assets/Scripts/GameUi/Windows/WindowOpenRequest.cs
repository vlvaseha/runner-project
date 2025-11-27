using System;

namespace GameUi
{
    public class WindowOpenRequest
    {
        public Type WindowType { get; }
        public WindowArguments Arguments { get; }

        public WindowOpenRequest(Type windowType, WindowArguments arguments)
        {
            WindowType = windowType;
            Arguments = arguments;
        }
    }
}