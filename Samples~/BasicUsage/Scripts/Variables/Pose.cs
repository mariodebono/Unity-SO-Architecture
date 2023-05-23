using System;
using UnityEngine;

namespace MarioDebono.Samples.BasicUsage
{
    /// <summary>
    /// An event argument that describes a pose
    /// </summary>
    [Serializable]
    public struct Pose
    {
        public Vector3 position;
        public Vector3 rotation;
    }
}