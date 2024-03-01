using System;
using UnityEngine;

namespace GameUi
{
    public abstract class WindowFactory
    {
        public abstract Window Create(Type windowType, Transform windowRoot);
    }
}