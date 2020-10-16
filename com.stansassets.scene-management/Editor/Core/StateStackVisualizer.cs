using System;
using System.Collections.Generic;

namespace StansAssets.SceneManagement
{
   public static class StateStackVisualizer
    {
        
        internal static readonly List<StateStackVisualizerItem> StackMap = new List<StateStackVisualizerItem>();

        public static void Register<T>(ApplicationStateStack<T> stack, string stackName) where T: Enum
        {

                StackMap.Add(new StateStackVisualizerItem<T>(stack, stackName));
        }
    }
}
