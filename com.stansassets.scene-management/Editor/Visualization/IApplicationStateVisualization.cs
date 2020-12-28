using System;

namespace StansAssets.SceneManagement
{
    public interface IApplicationStateVisualization<T> where T : Enum
    {
        void ChangeState(Actions evt);
    }
}
