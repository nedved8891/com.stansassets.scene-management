using System;
using System.Linq;
using UnityEngine.UIElements;

namespace StansAssets.SceneManagement
{
    abstract class StateStackVisualizerItem
    {
        /// <summary>
        /// 
        /// </summary>
        internal Action OnStackUpdated = delegate { };
        internal abstract string StackName();
        internal abstract bool IsBusy();
        internal abstract bool IsActive();

        internal abstract VisualElement UpdateStackUISetup();
    }

    class StateStackVisualizerItem<T> : StateStackVisualizerItem where T : Enum
    {
        readonly ApplicationStateStack<T> m_Stack;

        readonly string m_StackName;

        internal StateStackVisualizerItem(ApplicationStateStack<T> stack, string stackName)
        {
            m_Stack = stack;
            stack.SetVisualizerAction(OnStackUpdated);
            m_StackName = stackName;
        }

        /// <summary>
        /// 
        /// </summary>
        internal override string StackName() => m_StackName;

        /// <summary>
        /// 
        /// </summary>
        internal override bool IsBusy() => m_Stack.IsBusy;

        /// <summary>
        /// 
        /// </summary>
        internal override bool IsActive() => m_Stack.States.Any();

        internal override VisualElement UpdateStackUISetup()
        {
            var container = new VisualElement();

            var labelHeader = new Label { text = m_StackName };
            container.Add(labelHeader);
            
            var containerStates = new VisualElement();
            foreach (var state in m_Stack.States)
            {
                var label = new Label { text = state.ToString() };
                containerStates.Add(label);
            }
            container.Add(containerStates);
            
            return container;
        }
    }
}
