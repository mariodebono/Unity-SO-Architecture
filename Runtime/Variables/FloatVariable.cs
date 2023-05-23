using System;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Variables
{
    [CreateAssetMenu(menuName = "SO Architecture/Variables/Float Variable", fileName = "Float Variable")]
    public class FloatVariable : BaseVariable<float>
    {
    }

    [Serializable]
    public class FloatReference : BaseVariableReference<FloatVariable, float> { }
}
