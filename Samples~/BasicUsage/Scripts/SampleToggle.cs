using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioDebono.Samples.BasicUsage
{
    [AddComponentMenu("Samples/BasicUsage/Sample Toggle")]
    public class SampleToggle : MonoBehaviour
    {
        [SerializeField] VoidGameEvent gameEvent;

        // this component provides a button to raise the event at runtime
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 20), "Toggle"))
            {
                gameEvent.Raise();
            }
        }
    }
}
