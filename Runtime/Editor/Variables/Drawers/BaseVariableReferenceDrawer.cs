using MarioDebono.SOArchitecture.Variables;
using UnityEditor;
using UnityEngine;

namespace MarioDebono.SOArchitecture.Events
{
    [CustomPropertyDrawer(typeof(BaseVariableReference<,>), true)]
    public class BaseVariableReferenceDrawer : PropertyDrawer
    {
        enum ValueTypes { UseConstant, UseVariable }

        SerializedProperty useConstantProp;
        SerializedProperty valueProp;
        SerializedProperty varProp;

        // commented out because it is temporarily not used
        //SerializedProperty varPropObj;

        ValueTypes valueType;
        private bool constantHasChildren;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);

            if (constantHasChildren && valueType == ValueTypes.UseConstant)
                height += EditorGUI.GetPropertyHeight(valueProp, true);

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            valueProp = property.FindPropertyRelative("constantValue");
            varProp = property.FindPropertyRelative("variable");
            useConstantProp = property.FindPropertyRelative("useConstant");

            valueType = useConstantProp.boolValue ? ValueTypes.UseConstant : ValueTypes.UseVariable;

            // label
            EditorGUI.PrefixLabel(position, label);

            var fieldPosition = position;
            fieldPosition.x += EditorGUIUtility.labelWidth;
            fieldPosition.width = 20;
            fieldPosition.height = EditorGUIUtility.singleLineHeight;



            valueType = (ValueTypes)EditorGUI.EnumPopup(fieldPosition, valueType);

            var otherFieldPosition = position;
            // start from previous position to end of screen
            otherFieldPosition.x += EditorGUIUtility.labelWidth + 23;
            otherFieldPosition.width = EditorGUIUtility.currentViewWidth - otherFieldPosition.x;
            otherFieldPosition.height = EditorGUIUtility.singleLineHeight;

            switch (valueType)
            {
                case ValueTypes.UseConstant:
                    {
                        useConstantProp.boolValue = true;
                        constantHasChildren = valueProp.hasChildren;
                        if (!constantHasChildren)
                            EditorGUI.PropertyField(otherFieldPosition, valueProp, new GUIContent(""), false);
                        else
                            EditorGUI.LabelField(otherFieldPosition, "Complex Value");
                        break;
                    }
                case ValueTypes.UseVariable:
                    {
                        useConstantProp.boolValue = false;
                        EditorGUI.ObjectField(otherFieldPosition, varProp, new GUIContent(""));
                    }
                    break;
            }

            if (valueProp.hasChildren && useConstantProp.boolValue)
            {
                var ExpandedPosition = position;
                ExpandedPosition.y = EditorGUIUtility.singleLineHeight;
                ExpandedPosition.x += 5;
                ExpandedPosition.width = EditorGUIUtility.currentViewWidth - 5;
                EditorGUI.PropertyField(ExpandedPosition, valueProp, new GUIContent("Constant Value"), true);
            }
            // Undecided if the actual SO should be expanded here and lose the one-line design or not

            // expand the object for editing
            //if (varProp.objectReferenceValue != null && valueType == ValueTypes.UseVariable)
            //{
            //    varPropObj = new SerializedObject(varProp.objectReferenceValue).FindProperty("value");
            //    EditorGUI.BeginChangeCheck();

            //    EditorGUILayout.PropertyField(varPropObj, true);

            //    if (EditorGUI.EndChangeCheck())
            //    {
            //        (varProp.objectReferenceValue as dynamic).value = varPropObj.boxedValue as dynamic;
            //    }

            //    if (EditorGUI.EndChangeCheck())
            //    {

            //    }

            //}
        }
    }
}
