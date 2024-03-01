using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowArguments
{
    #region Properties

    public WindowType Type { get; }

    #endregion

    #region Class lifecycle

    public WindowArguments(WindowType type)
    {
        Type = type;
    }

    #endregion
}
