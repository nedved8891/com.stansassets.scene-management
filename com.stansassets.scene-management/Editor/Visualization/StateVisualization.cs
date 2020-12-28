using UnityEngine;

namespace StansAssets.SceneManagement
{
    public class StateVisualization : IApplicationStateVisualization<Actions>
    {
        public void ChangeState(Actions evt)
        {
            switch (evt)
            {
                case Actions.Added:
                    Debug.Log("StackAction.Added: ");
                    break;
                case Actions.Paused:
                    Debug.Log("StackAction.Paused: " );
                    break;
                case Actions.Resumed:
                    Debug.Log("StackAction.Resumed: " );
                    break;
                default:
                    Debug.Log("StackAction.Removed: ");
                    break;
            }
        }
    }
}
