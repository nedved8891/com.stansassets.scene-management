using System;
using System.Collections.Generic;

namespace StansAssets.SceneManagement
{
    public interface IApplicationStateStackVisualization<T> : IReadOnlyApplicationStateStackVizualization<T> where T : Enum
    {
        void RegisterState(string key, IApplicationStateVisualization<T> applicationState);

        void ChangeState(string key, T action);

        Dictionary<string, Actions> GetState();
    }
}
