using System;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Variables
{

    /// <summary>
    /// Provides a base for a variable reference
    /// </summary>
    /// <typeparam name="TVar"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public abstract class BaseVariableReference<TVar, TValue>
        where TVar : BaseVariable<TValue>
        //where TValue : struct
    {
        [SerializeField] bool useConstant = false;
        [SerializeField] public TValue constantValue;
        [SerializeField] public TVar variable = default;

        /// <summary>
        /// Get or set the value based on the editor setting to use constant or variable value
        /// </summary>
        public TValue Value
        {
            get => useConstant ? constantValue : variable.value;
            set
            {
                if (useConstant)
                    constantValue = value;
                else
                    variable.value = value;
            }
        }
    }
}
