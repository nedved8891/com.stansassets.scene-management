//#if UNITY_2019_4_OR_NEWER
using StansAssets.Plugins.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace StansAssets.SceneManagement
{
    public enum Actions
    {
        Added,
        Removed,
        Paused,
        Resumed
    }

    public class SettingsTab : BaseTab
    {
        Dictionary<string, string> someDictionary = new Dictionary<string, string>();

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

            PopulatePresetList();

            SetupControls();
        }

        private void PopulatePresetList()
        {
            var list = Root.Q<ListView>("ListView");
            list.Clear();

            foreach (KeyValuePair<string, string> p in someDictionary.Reverse())
            {
                Label listLabel = new Label(p.Key);
                listLabel.name = GetStyle(p.Value);
                list.Add(listLabel);
            }
        }

        private string GetStyle(string act)
        {
            switch (act)
            {
                case "Added":
                    return "LabelActive";
                case "Paused":
                    return "LabelPause";
                case "Resumed":
                    return "LabelActive";
                default:
                    return "LabelDelete";
            }
        }

        private void ChangeState(string name, string act)
        {
            Debug.Log("ChangeState");
            if (act == "Added")
            {
                someDictionary.Add(name, act);
            }

            if (act == "Removed")
            {
                Debug.Log(name + " -> " + act);
                someDictionary.Remove(name);
            }
            else
            {
                someDictionary[name] = act;
            }

            PopulatePresetList();
        }

        public delegate void ChangeStateDelegate(string name, Actions action);
        public static ChangeStateDelegate OnChangeStateDelegate;

        private void SetupControls()
        {
            Button addedButton = Root.Q<Button>("Added");
            Button removedButton = Root.Q<Button>("Removed");
            Button pausedButton = Root.Q<Button>("Paused");
            Button resumedButton = Root.Q<Button>("Resumed");

            addedButton.clickable.clicked += () =>
            {
                OnChangeStateDelegate?.Invoke("Game", Actions.Added);
            };

            pausedButton.clickable.clicked += () =>
            {
                OnChangeStateDelegate?.Invoke("Game", Actions.Paused);
            };

            resumedButton.clickable.clicked += () =>
            {
                OnChangeStateDelegate?.Invoke("Game", Actions.Resumed);
            };

            removedButton.clickable.clicked += () =>
            {
                OnChangeStateDelegate?.Invoke("Game", Actions.Removed);
            };
        }
    }
}
//#endif