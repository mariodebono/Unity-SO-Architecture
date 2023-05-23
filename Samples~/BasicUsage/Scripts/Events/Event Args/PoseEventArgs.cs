using System;
using UnityEngine;

namespace MarioDebono.Samples.BasicUsage.EventArgs
{
    /// <summary>
    /// An event argument that describes a pose
    /// </summary>
    [Serializable]
    public struct PoseEventArgs
    {
        public Vector3 position;
        public Vector3 rotation;
    }
}