using UnityEngine;
using MarioDebono.Events;
using MarioDebono.Samples.BasicUsage.EventArgs;

namespace MarioDebono.Samples.BasicUsage
{
    [CreateAssetMenu(menuName = "Samples/SO Architecture BasicUsage/Pose Event")]
    public class PoseGameEvent : BaseGameEvent<PoseEventArgs>
    {
    }
}