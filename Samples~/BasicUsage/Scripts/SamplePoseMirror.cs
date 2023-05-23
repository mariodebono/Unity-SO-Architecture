using MarioDebono.Samples.BasicUsage.EventArgs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioDebono.Samples.BasicUsage
{
    [AddComponentMenu("Samples/SO Architecture BasicUsage/Sample Pose Var Mirror")]
    public class SamplePoseMirror : MonoBehaviour
    {
        [SerializeField] PoseReference pose;
        [SerializeField] FloatReference smoothTime;

        Vector3 posVelocity;
        float rotVelocityX, rotVelocityY, rotVelocityZ;

        public void Update()
        {
            var value = pose.Value;
            // mirror on the x-axis the received pose
            transform.position = Vector3.SmoothDamp(transform.position,
                new(-value.position.x, value.position.y, value.position.z), ref posVelocity, smoothTime.Value);

            var rot = transform.eulerAngles;
            var newRotX = Mathf.SmoothDampAngle(rot.x, value.rotation.x, ref rotVelocityX, smoothTime.Value);
            var newRotY = Mathf.SmoothDampAngle(rot.y, value.rotation.y, ref rotVelocityY, smoothTime.Value);
            var newRotZ = Mathf.SmoothDampAngle(rot.z, value.rotation.z, ref rotVelocityZ, smoothTime.Value);

            transform.eulerAngles = new(newRotX, newRotY, newRotZ);
        }

    }
}
