using System;
using System.Collections.Generic;

namespace StansAssets.SceneManagement
{
    public interface IReadOnlyApplicationStateStackVizualization<T> where T : Enum
    {
        void Set(string state, Action act);
    }
}
