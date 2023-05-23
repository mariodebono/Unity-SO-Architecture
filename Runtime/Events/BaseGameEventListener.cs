using UnityEngine;
using UnityEngine.Events;

namespace MarioDebono.Events
{
    /// <summary>
    /// Provides a base event listener to receive events with generic arguments
    /// </summary>
    /// <typeparam name="TArgs">The arguments that is expected to be received from the event</typeparam>
    public abstract class BaseGameEventListener<TArgs> : MonoBehaviour
       where TArgs : struct
    {
        [Tooltip("The game event type to listen for")]
        [SerializeField] protected BaseGameEvent<TArgs> gameEvent;
        [Tooltip("An event to trigger in response of a raised event")]
        [SerializeField] protected UnityEvent<TArgs> eventResponse;

        [Tooltip("A serialized arguments field to be used in the inspector to test the event locally")]
        [SerializeField] TArgs args;

        /// <summary>
        /// This method is called when the referenced event is raised
        /// </summary>
        /// <param name="args">The args received in the event</param>
        public virtual void OnEventRaised(TArgs args)
        {
            eventResponse.Invoke(args);
        }

        protected virtual void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        protected void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }
    }


}
