// Ignore Spelling: Debono

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Events
{
    /// <summary>
    /// Provides the base structure to create generic events
    /// </summary>
    /// <typeparam name="TArgs">A Struct type to be passed as arguments</typeparam>
    public abstract class BaseGameEvent<TArgs> : ScriptableObject
        where TArgs : struct
    {
        readonly List<BaseGameEventListener<TArgs>> listeners = new();

        public event Action<TArgs> OnRaised = delegate { };
#pragma warning disable CS0414 // Remove unread private members, used by the editor
#pragma warning disable IDE0052 // Remove unread private members, used by the editor
        [SerializeField] TArgs args = default;
#pragma warning restore IDE0052 // Remove unread private members, used by the editor
#pragma warning restore CS0414 // Remove unread private members, used by the editor

        /// <summary>
        /// Register a listener to this event
        /// </summary>
        /// <param name="listener">The listener to be called when this event is raised</param>
        public void RegisterListener(BaseGameEventListener<TArgs> listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }
        /// <summary>
        /// Unregister a listener from this event
        /// </summary>
        /// <param name="listener">The listener that was previously registered</param>
        public void UnregisterListener(BaseGameEventListener<TArgs> listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        /// <summary>
        /// Raise the event on all registered listeners
        /// </summary>
        /// <param name="args">The arguments to be passed with the event</param>
        public void Raise(TArgs args)
        {

            OnRaised.Invoke(args);
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                try
                {
                    listeners[i].OnEventRaised(args);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
    }


}
