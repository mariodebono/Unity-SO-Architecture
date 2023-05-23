using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioDebono.Samples
{
    [AddComponentMenu("Samples/BasicUsage/Toggle Active")]
    public class ToggleActive : MonoBehaviour
    {

        public void OnToggle()
        {
            // toggle the gameObject active state
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
    }
}
