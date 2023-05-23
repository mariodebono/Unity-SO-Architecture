using MarioDebono.Samples.BasicUsage.EventArgs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioDebono.Samples.BasicUsage
{
    [AddComponentMenu("Samples/SO Architecture BasicUsage/Sample Mirror Apply")]
    public class SampleMirrorPosRotApply : MonoBehaviour
    {
        
        public void OnPose(PoseEventArgs args)
        {
            // mirror on the x-axis the received pose
            transform.position = new (-args.position.x, args.position.y, args.position.z);
            transform.eulerAngles = new(args.rotation.x, -args.rotation.y, -args.rotation.z);
        }

    }
}
