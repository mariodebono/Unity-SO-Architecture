using UnityEngine;

namespace MarioDebono.Variables
{
    /// <summary>
    /// Provides the base for a generic variable type
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public abstract class BaseVariable<T> : ScriptableObject
        where T : struct
    {
        public T value = default;
    }
}
