using MarioDebono.Samples.BasicUsage.EventArgs;
using UnityEngine;

namespace MarioDebono.Samples.BasicUsage
{
    [AddComponentMenu("Samples/SO Architecture BasicUsage/Sample Mirror Source")]
    public class SampleMirrorPosRotSource : MonoBehaviour
    {
        [SerializeField] PoseGameEvent gameEvent;

        private void Update()
        {

            // Raise a pose event every frame
            gameEvent.Raise(new PoseEventArgs
            {
                position = transform.position,
                rotation = transform.eulerAngles
            });
        }

    }
}
