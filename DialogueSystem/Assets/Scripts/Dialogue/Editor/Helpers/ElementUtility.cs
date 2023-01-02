using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
    public static class ElementUtility
    {
        public static Group CreateGroup(string groupName, List<ISelectable> graphSelection)
        {
            Group group = new Group()
            {
                title = groupName,
            };

            group.SetPosition(new Rect(Vector2.zero, Vector2.one));

            foreach (GraphElement selectedElement in graphSelection)
            {
                if ((selectedElement is not GraphNode)) continue;

                group.AddElement(selectedElement);
            }

            return group;
        }
        
        public static void AddStyleClasses(this VisualElement element, params string[] classes)
        {
            foreach (string styleClass in classes)
            {
                element.AddToClassList(styleClass);
            }
        }

        public static void AddStyleSheets(this VisualElement element, params string[] styleSheetNames)
        {
            foreach (string styleSheet in styleSheetNames)
            {
                StyleSheet stylesheet = (StyleSheet)EditorGUIUtility.Load(styleSheet);
                element.styleSheets.Add(stylesheet);
            }
        }

        public static Port CreatePort(this GraphNode node, string portName, PortSettings portSettings)
        {
            Port port = node.InstantiatePort(portSettings.orientation, portSettings.direction, portSettings.capacity, typeof(bool));
            port.portName = portName;

            return port;
        }

        public static ObjectField CreateObjectField(GameObject value = null)
        {
            return new ObjectField()
            {
                value = value,
                objectType = typeof(GameObject),
                allowSceneObjects = false,
            };
        }
        
        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };

            return button;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static void OnValueChanged(this TextField textField, params Action<string>[] callbacks)
        {
            textField.RegisterValueChangedCallback(x =>
            {
                foreach (Action<string> callback in callbacks)
                    callback(x.newValue);
            });
        }

        public static void OnValueChanged(this ObjectField objectField, params Action<GameObject>[] callbacks)
        {
            objectField.RegisterValueChangedCallback(x =>
            {
                foreach (Action<GameObject> callback in callbacks)
                    callback((GameObject) x.newValue);
            });
        }

        public static void OnValueChanged(this EnumField enumField, params Action<Enum>[] callbacks)
        {
            enumField.RegisterValueChangedCallback(x =>
            {
                foreach (Action<Enum> callback in callbacks)
                    callback(x.newValue);
            });
        }

        public static TextField CreateTextField(string value = null)
        {
            TextField textField = new TextField()
            {
                value = value
            };

            return textField;
        }

        public static Label CreateLabel(string value = "")
        {
            Label label = new Label(value);
            return label;
        }

        public static TextField CreateTextArea(string value = null)
        {
            TextField textArea = CreateTextField(value);
            textArea.multiline = true;
            return textArea;
        }

    }

    public readonly struct PortSettings
    {
        public readonly Orientation orientation;
        public readonly Direction direction;
        public readonly Port.Capacity capacity;

        public PortSettings(Orientation ori, Direction dir, Port.Capacity cap)
        {
            orientation = ori;
            direction = dir;
            capacity = cap;
        }
    }

}
