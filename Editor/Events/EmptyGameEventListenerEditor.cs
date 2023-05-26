using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MarioDebono.SOArchitecture.Events
{
    [CustomEditor(typeof(EmptyGameEventListener), true)]
    public class EmptyGameEventListenerEditor : Editor
    {
        EmptyGameEventListener gameEventListener;

        VisualElement constantContainer;

        PropertyField eventField;
        SerializedProperty eventPorp;

        Button raiseButton;
        Button raiseAllButton;

        private void OnEnable()
        {
            gameEventListener = target as EmptyGameEventListener;
        }

        private void OnDisable()
        {
            if (raiseButton != null)
                raiseButton.UnregisterCallback<ClickEvent>(RaiseEventClicked);
            if (raiseAllButton != null)
                raiseAllButton.UnregisterCallback<ClickEvent>(RaiseAllEventClicked);

            gameEventListener = null;
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            constantContainer = new Box();
            constantContainer.style.marginBottom = 10;

            raiseAllButton = new Button();
            raiseAllButton.style.marginTop = 10;
            raiseAllButton.text = $"Raise Event";
            raiseAllButton.RegisterCallback<ClickEvent>(RaiseAllEventClicked);

            raiseButton = new Button();
            raiseButton.text = $"Raise Local Event";
            raiseButton.RegisterCallback<ClickEvent>(RaiseEventClicked);
            raiseButton.style.marginBottom = 5;

            constantContainer.Add(raiseAllButton);
            constantContainer.Add(raiseButton);

            var iterator = serializedObject.GetIterator();
            if (iterator.NextVisible(true))
            {
                do
                {
                    var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

                    //  disable script field
                    if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                        propertyField.SetEnabled(value: false);

                    if (iterator.propertyPath == "gameEvent" && serializedObject.targetObject != null)
                    {
                        eventField = propertyField;
                        eventPorp = iterator.Copy();
                        eventField.TrackSerializedObjectValue(eventPorp.serializedObject, (o) =>
                        {
                            eventField.Bind(eventPorp.serializedObject);
                        });

                        container.Add(propertyField);
                        container.Add(constantContainer);
                    }
                    else
                        container.Add(propertyField);
                }
                while (iterator.NextVisible(false));
            }

            return container;
        }

        private void RaiseAllEventClicked(ClickEvent evt)
        {
            gameEventListener.RaiseAttachedGameEvent();
        }

        private void RaiseEventClicked(ClickEvent evt)
        {
            gameEventListener.OnEventRaised();
        }
    }
}
