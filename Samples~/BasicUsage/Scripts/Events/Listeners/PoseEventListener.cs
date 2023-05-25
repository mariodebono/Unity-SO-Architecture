using UnityEngine;
using MarioDebono.SOArchitecture.Events;
using MarioDebono.Samples.BasicUsage.EventArgs;

namespace MarioDebono.Samples.BasicUsage
{
    [AddComponentMenu("Samples/SO Architecture BasicUsage/Pose Event Listener")]
    public class PoseEventListener : BaseGameEventListener<PoseEventArgs>
    {
        public override void OnEventRaised(PoseEventArgs args)
        {
            // extend the logic in this method
            // event will not trigger if you don't call the base
            // you can access the base event and action here also
            base.OnEventRaised(args);
        }
    }
}