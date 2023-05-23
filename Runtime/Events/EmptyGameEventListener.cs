using UnityEngine;
using UnityEngine.Events;

namespace MarioDebono.Events
{
    [AddComponentMenu("SO Architecture/Events/Empty Event Listener")]
    public class EmptyGameEventListener : MonoBehaviour
    {
        [Tooltip("The game event type to listen for")]
        [SerializeField] EmptyGameEvent gameEvent;
        [Tooltip("An event to trigger in response of a raised event")]
        [SerializeField] UnityEvent eventResponse;

        /// <summary>
        /// This method is called when the referenced event is raised
        /// </summary>
        public virtual void OnEventRaised()
        {
            eventResponse.Invoke();
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
