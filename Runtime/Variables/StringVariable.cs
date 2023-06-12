using System;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Variables
{
    [CreateAssetMenu(menuName = "SO Architecture/Variables/String Variable", fileName = "String Variable")]
    public class StringVariable : BaseVariable<string>
    {
    }

    [Serializable]
    public class StringReference : BaseVariableReference<StringVariable, string>
    {

    }

}
