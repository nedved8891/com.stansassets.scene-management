#if UNITY_2019_4_OR_NEWER
using System;
using StansAssets.Plugins.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.SceneManagement
{
    public class SettingsTab : BaseTab
    {
        readonly VisualElement m_Enums;

        public SettingsTab()
            : base($"{SceneManagementPackage.WindowTabsPath}/SettingsTab")
        {
            var landingSceneField = Root.Q<ObjectField>("landing-scene");
            landingSceneField.objectType = typeof(SceneAsset);
            landingSceneField.SetValueWithoutNotify(SceneManagementSettings.Instance.LandingScene);

            landingSceneField.RegisterValueChangedCallback((e) =>
            {
                SceneManagementSettings.Instance.LandingScene = (SceneAsset)e.newValue;
                SceneManagementSettings.Save();
            });

            m_Enums = this.Q<VisualElement>("enums");
            foreach (var stack in StateStackVisualizer.StackMap)
            {
                stack.OnStackUpdated += () => { StackUpdated(stack); };
                StackUpdated(stack);
            }
        }

        void StackUpdated(StateStackVisualizerItem stack)
        {
            if (stack.IsActive())
            {
                var stackUI = stack.UpdateStackUISetup();
                m_Enums.Add(stackUI);
            }
        }
    }
}
#endif
