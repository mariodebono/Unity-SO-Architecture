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

        ValueTypes valueType;
        bool constantHasChildren;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);

            valueProp = property.FindPropertyRelative("constantValue");
            varProp = property.FindPropertyRelative("variable");
            useConstantProp = property.FindPropertyRelative("useConstant");

            // Handle Strings single Line
            if (GUI.enabled && (constantHasChildren && valueProp.type != "string") && useConstantProp.boolValue)
            {
                height += EditorGUI.GetPropertyHeight(valueProp, valueProp.isExpanded);
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            valueProp = property.FindPropertyRelative("constantValue");
            varProp = property.FindPropertyRelative("variable");
            useConstantProp = property.FindPropertyRelative("useConstant");

            if (!GUI.enabled)
            {
                valueType = ValueTypes.UseVariable;
                useConstantProp.boolValue = false;
            }
            else
            {
                valueType = useConstantProp.boolValue ? ValueTypes.UseConstant : ValueTypes.UseVariable;
            }

            var labelPosition = position;
            labelPosition.width = EditorGUIUtility.labelWidth;

            if (valueProp.hasChildren && useConstantProp.boolValue)
            {
                GUI.Box(position, "");
            }

            // label
            EditorGUI.PrefixLabel(labelPosition, new GUIContent(property.displayName));

            var fieldPosition = position;
            fieldPosition.x = position.x + EditorGUIUtility.labelWidth;
            fieldPosition.width = 20;
            fieldPosition.height = EditorGUIUtility.singleLineHeight;

            valueType = (ValueTypes)EditorGUI.EnumPopup(fieldPosition, valueType);
            useConstantProp.boolValue = (valueType == ValueTypes.UseConstant) ? true : false;

            var otherFieldPosition = position;
            // start from previous position to end of screen
            otherFieldPosition.x += EditorGUIUtility.labelWidth + 23;
            otherFieldPosition.width = EditorGUIUtility.currentViewWidth - otherFieldPosition.x - 5;
            otherFieldPosition.height = EditorGUIUtility.singleLineHeight;

            switch (valueType)
            {
                case ValueTypes.UseConstant:
                    {
                        constantHasChildren = valueProp.hasChildren;
                        // Handle Strings single Line
                        if (!constantHasChildren || valueProp.type == "string")
                        {
                            var tmpWidth = EditorGUIUtility.labelWidth;
                            GUIContent contentLabel = new GUIContent();
                            switch (valueProp.type)
                            {
                                case "int":
                                case "float":
                                    EditorGUIUtility.labelWidth = 18;
                                    contentLabel = EditorGUIUtility.IconContent("d_Preset.Context");
                                    break;
                                default:
                                    break;

                            }
                            EditorGUI.PropertyField(otherFieldPosition, valueProp, contentLabel, false);
                            EditorGUIUtility.labelWidth = tmpWidth;
                        }
                        else
                            EditorGUI.LabelField(otherFieldPosition, "Complex Value");
                        break;
                    }
                case ValueTypes.UseVariable:
                    {
                        EditorGUI.ObjectField(otherFieldPosition, varProp, new GUIContent(""));
                    }
                    break;
            }
            // Handle Strings single Line
            if ((valueProp.hasChildren && valueProp.type != "string") && useConstantProp.boolValue)
            {

                var expandedPosition = position;
                expandedPosition.y = position.y + EditorGUIUtility.singleLineHeight;
                expandedPosition.x += 12; // indent to fit under label
                expandedPosition.width = EditorGUIUtility.currentViewWidth - expandedPosition.x - 5; // keep 5 from edge

                var tmpWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = tmpWidth - 12; // moved 5 above

                var foldoutPosition = expandedPosition;
                foldoutPosition.height = EditorGUIUtility.singleLineHeight;

                // default inspector
                EditorGUI.PropertyField(expandedPosition, valueProp, new GUIContent(valueProp.displayName), true);

                property.serializedObject.ApplyModifiedProperties();
            }
        }

    }
}