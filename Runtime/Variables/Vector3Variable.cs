using System;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Variables
{
    [CreateAssetMenu(menuName = "SO Architecture/Variables/Vector3 Variable", fileName = "Vector3 Variable")]
    public class Vector3Variable : BaseVariable<Vector3>
    {
    }

    [Serializable]
    public class Vector3Reference : BaseVariableReference<Vector3Variable, Vector3> { }
}
