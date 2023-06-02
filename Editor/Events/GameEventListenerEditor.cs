using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MarioDebono.SOArchitecture.Events
{
    [CustomEditor(typeof(BaseGameEventListener<>), true)]
    public class GameEventListenerEditor : Editor
    {
        dynamic gameEventListener;
        dynamic gameEvent;

        VisualElement constantContainer;

        PropertyField eventField;
        PropertyField argField;

        SerializedProperty argProperty;

        Button raiseButton;
        Button raiseAllButton;

        private void OnEnable()
        {
            gameEventListener = target;
        }

        private void OnDisable()
        {
            if (eventField != null)
                eventField.UnregisterCallback<SerializedPropertyChangeEvent>(GameEventChanged);
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

                    if (iterator.propertyPath == "args" && serializedObject.targetObject != null)
                    {
                        argProperty = iterator.Copy();
                        argField = propertyField;
                        argField.TrackSerializedObjectValue(argProperty.serializedObject, (o) =>
                        {
                            argField.Bind(argProperty.serializedObject);
                        });
                        constantContainer.Insert(0, propertyField);
                    }
                    else if (iterator.propertyPath == "gameEvent" && serializedObject.targetObject != null)
                    {
                        eventField = propertyField;
                        propertyField.RegisterValueChangeCallback(GameEventChanged);

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

            var args = (dynamic)argProperty.boxedValue;
            gameEventListener.RaiseAttachedGameEvent(args);

        }

        private void RaiseEventClicked(ClickEvent evt)
        {
            var args = (dynamic)argProperty.boxedValue;
            gameEventListener.OnEventRaised(args);
        }

        private void GameEventChanged(SerializedPropertyChangeEvent evt)
        {
            var obj = evt.changedProperty.objectReferenceValue;

            if (obj != null)
            {
                gameEvent = obj;
                constantContainer.style.display = DisplayStyle.Flex;
            }
            else
            {
                gameEvent = null;
                constantContainer.style.display = DisplayStyle.None;
            }

        }
    }
}
