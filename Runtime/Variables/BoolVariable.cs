using System;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Variables
{
    [CreateAssetMenu(menuName = "SO Architecture/Variables/Bool Variable", fileName = "Bool Variable")]
    public class BoolVariable : BaseVariable<bool>
    {
    }

    [Serializable]
    public class BoolReference : BaseVariableReference<BoolVariable, bool> { }
}
