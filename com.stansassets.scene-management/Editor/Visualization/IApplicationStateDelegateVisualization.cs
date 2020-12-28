using System;

namespace StansAssets.SceneManagement
{
    public interface IApplicationStateDelegateVisualization<T> where T : Enum
    {
        void ApplicationStateChanged(Actions e);
    }
}
