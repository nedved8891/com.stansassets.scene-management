//#if UNITY_2019_4_OR_NEWER
using StansAssets.Plugins.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Label = UnityEngine.UIElements.Label;

namespace StansAssets.SceneManagement
{
    public enum Actions
    {
        None,
        Added,
        Removed,
        Paused,
        Resumed
    }

    public class SettingsTab : BaseTab
    {
        private Dictionary<string, Actions> someDictionary = new Dictionary<string, Actions>();

        public static IApplicationStateStackVisualization<Actions> StateVisualization { get; } = new ApplicationStateStackVisualization<Actions>();
        
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
            
            //StateVisualization.RegisterState("MainMenu", Actions.Added);
            //StateVisualization.RegisterState("Game",Actions.Added);
            
            PopulatePresetList();

            SetupControls();
        }

        private void PopulatePresetList()
        {
            Debug.Log("###");
            var list = Root.Q<ListView>("ListView");
            list.Clear();

            foreach (var st in StateVisualization.GetState())
            {
                var listLabel = new Label(st.Key) {name = GetStyle(st.Value)};
                if (st.Value != Actions.Removed)
                    list.Add(listLabel);
            }
        }

        private string GetStyle(Actions act)
        {
            switch (act)
            {
                case Actions.Added:
                    return "LabelActive";
                case Actions.Paused:
                    return "LabelPause";
                case Actions.Resumed:
                    return "LabelActive";
                case Actions.Removed:
                    return "LabelDelete";
                default:
                    return "Label";
            }
        }

        private void ChangeState(string name, Actions act)
        {
            if (act == Actions.Added)
            {
                someDictionary.Add(name, act);
            }

            if (act == Actions.Removed)
            {
                someDictionary.Remove(name);
            }
            else
            {
                someDictionary[name] = act;
            }

            PopulatePresetList();
        }
        
        private void SetupControls()
        {
            var registerButton = Root.Q<Button>("Register");
            var addedButton = Root.Q<Button>("Added");
            var removedButton = Root.Q<Button>("Removed");
            var pausedButton = Root.Q<Button>("Paused");
            var resumedButton = Root.Q<Button>("Resumed");
            
            registerButton.clickable.clicked += () =>
            {
                //StateVisualization.RegisterState("Game",Actions.None, PopulatePresetList);
                StateVisualization.RegisterState("Game", new StateVisualization());
            };

            addedButton.clickable.clicked += () =>
            {
                StateVisualization.Set("Game", PopulatePresetList);
            };

            pausedButton.clickable.clicked += () =>
            {
                StateVisualization.Set("Game", PopulatePresetList);
            };

            resumedButton.clickable.clicked += () =>
            {
                StateVisualization.Set("Game", PopulatePresetList);
            };

            removedButton.clickable.clicked += () =>
            {
                StateVisualization.Set("Game", PopulatePresetList);
            };
        }
    }
}
//#endif