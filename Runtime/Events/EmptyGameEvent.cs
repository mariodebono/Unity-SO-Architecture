using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarioDebono.Events
{

    [CreateAssetMenu(menuName = "SO Architecture/Events/Empty Game Event", order = -100)]
    public class EmptyGameEvent : ScriptableObject
    {
        readonly List<EmptyGameEventListener> listeners = new();

        public event Action OnRaised = delegate { };

        /// <summary>
        /// Register a listener to this event
        /// </summary>
        /// <param name="listener">The listener to be called when this event is raised</param>
        public void RegisterListener(EmptyGameEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }
        /// <summary>
        /// Unregister a listener from this event
        /// </summary>
        /// <param name="listener">The listener that was previously registered</param>
        public void UnregisterListener(EmptyGameEventListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        /// <summary>
        /// Raise the event on all registered listeners
        /// </summary>
        public void Raise()
        {

            OnRaised.Invoke();
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                try
                {
                    listeners[i].OnEventRaised();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
    }


}
