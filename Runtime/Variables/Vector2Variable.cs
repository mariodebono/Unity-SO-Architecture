using System;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Variables
{
    [CreateAssetMenu(menuName = "SO Architecture/Variables/Vector2 Variable", fileName = "Vector2 Variable")]
    public class Vector2Variable : BaseVariable<Vector2>
    {
    }

    [Serializable]
    public class Vector2Reference : BaseVariableReference<Vector2Variable, Vector2> { }
}
