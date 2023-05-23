using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MarioDebono.Events
{
    [CustomEditor(typeof(BaseGameEvent<>), true)]
    public class GameEventEditor : Editor
    {
        dynamic gameEvent;

        VisualElement constantContainer;

        Button raiseButton;

        PropertyField argField;
        SerializedProperty argProperty;

        private void OnEnable()
        {
            gameEvent = target;
        }

        private void OnDisable()
        {
            if (raiseButton != null)
                raiseButton.UnregisterCallback<ClickEvent>(OnRaiseEvent);
        }

        private void OnRaiseEvent(ClickEvent evt)
        {
            gameEvent.Raise((dynamic)argProperty.boxedValue);
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            constantContainer = new Box();
            constantContainer.style.marginBottom = 10;

            raiseButton = new Button();
            raiseButton.text = $"Raise Local Event";
            raiseButton.RegisterCallback<ClickEvent>(OnRaiseEvent);
            raiseButton.style.marginBottom = 5;

            constantContainer.Add(raiseButton);

            var iterator = serializedObject.GetIterator();
            if (iterator.NextVisible(true))
            {
                do
                {
                    var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

                    // disable script field
                    if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                        propertyField.SetEnabled(value: false);

                    if (iterator.propertyPath == "args" && serializedObject.targetObject != null)
                    {
                        argProperty = iterator.Copy();
                        argField = propertyField;
                        argField.TrackSerializedObjectValue(argProperty.serializedObject, (o) =>
                        {
                            argField.Bind(argProperty.serializedObject);
                        });
                        constantContainer.Insert(0, propertyField);

                        container.Add(constantContainer);
                    }
                    else
                        container.Add(propertyField);
                }
                while (iterator.NextVisible(false));
            }

            return container;

        }
    }
}
