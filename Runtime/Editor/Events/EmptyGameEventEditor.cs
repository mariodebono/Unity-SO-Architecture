using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MarioDebono.Events
{
    [CustomEditor(typeof(EmptyGameEvent), true)]
    public class EmptyGameEventEditor : Editor
    {
        EmptyGameEvent gameEvent;

        VisualElement constantContainer;

        private void OnEnable()
        {
            gameEvent = target as EmptyGameEvent;
        }

        private void OnRaiseEvent(ClickEvent evt)
        {
            gameEvent.Raise();
        }


        public override VisualElement CreateInspectorGUI()
        {
            var so = new SerializedObject(target);

            constantContainer = new VisualElement();

            var label = new Label("Editor Only");
            label.style.unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold);
            label.style.marginBottom = 10;
            label.style.marginLeft = 5;
            label.style.marginRight = 5;
            label.style.marginTop = 10;

            constantContainer.Add(label);

            var raiseButton = new Button();
            raiseButton.style.marginTop = 10;
            raiseButton.text = $"Raise {gameEvent.name}";
            raiseButton.RegisterCallback<ClickEvent>(OnRaiseEvent);
            constantContainer.Add(raiseButton);

            return constantContainer;

        }



    }
}
