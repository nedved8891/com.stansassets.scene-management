using JetBrains.Annotations;
using StansAssets.Foundation.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace StansAssets.SceneManagement
{
    public class ApplicationStateStackVisualization<T> : IApplicationStateStackVisualization<T> where T : Enum
    {
        public static Dictionary<string, IApplicationStateVisualization<T>> m_EnumToState = new Dictionary<string, IApplicationStateVisualization<T>>();
        public static Dictionary<string, Actions> m_EnumToState2 = new Dictionary<string, Actions>();

        Action m_Subscriptions;
        
        public ApplicationStateStackVisualization()
        {
        }

        /*public void RegisterState(string key, T action, Action act)
        {
            Debug.Log(action + "   " + key);
            m_EnumToState.Add(key, action);
            AddDelegate(act);
        }*/
        
        public void RegisterState(string key, IApplicationStateVisualization<T> applicationState)
        {
            Debug.Log("Register:   " + key);
            m_EnumToState.Add(key, applicationState);
            m_EnumToState2.Add(key, Actions.None);
        }
        
        public void AddDelegate(Action d)
        {
            m_Subscriptions = d;
        }

        public void ChangeState(string key, T action)
        {
            m_EnumToState2[key] = Actions.Added;
            
            m_Subscriptions?.Invoke();
        }

        public Dictionary<string, Actions> GetState()
        {
            return m_EnumToState2;
        }
        
        public void Set(string applicationState, [NotNull] Action onComplete)
        {
            Debug.Log("Set");
            m_EnumToState2[applicationState] = Actions.Added;
            onComplete.Invoke();
        }
    }
}
