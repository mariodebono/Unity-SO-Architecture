using System;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Variables
{
    [CreateAssetMenu(menuName = "SO Architecture/Variables/Int Variable", fileName = "Int Variable")]
    public class IntVariable : BaseVariable<int>
    {
    }

    [Serializable]
    public class IntReference : BaseVariableReference<IntVariable, int> { }
}
