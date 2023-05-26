using UnityEngine;
using UnityEngine.Events;

namespace MarioDebono.SOArchitecture.Events
{
    [AddComponentMenu("SO Architecture/Events/Empty Event Listener")]
    public class EmptyGameEventListener : MonoBehaviour
    {
        [Tooltip("The game event type to listen for")]
        [SerializeField] protected EmptyGameEvent gameEvent;
        [Tooltip("An event to trigger in response of a raised event")]
        [SerializeField] protected UnityEvent eventResponse;

        /// <summary>
        /// This method is called when the referenced event is raised
        /// </summary>
        public virtual void OnEventRaised()
        {
            eventResponse.Invoke();
        }

        public void RaiseAttachedGameEvent()
        {
            if (gameEvent != null)
            {
                gameEvent.Raise();
            }
        }

        private void OnEnable()
        {
            if (gameEvent != null)
                gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent != null)
                gameEvent.UnregisterListener(this);
        }
    }


}
