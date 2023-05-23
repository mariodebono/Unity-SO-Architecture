using MarioDebono.Samples.BasicUsage.EventArgs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioDebono.Samples.BasicUsage
{
    [AddComponentMenu("Samples/SO Architecture BasicUsage/Sample Pose Source")]
    public class SamplePoseSource : MonoBehaviour
    {
        [SerializeField] PoseReference pose;

        public void Update()
        {
            pose.Value = new Pose
            {
                position = transform.position,
                rotation = transform.eulerAngles
            };

        }

    }
}
