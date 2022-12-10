using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
    public static class ElementUtility
    {
        public static Group CreateGroup(string groupName, Vector2 mousePosition, List<ISelectable> graphSelection)
        {
            Group group = new Group()
            {
                title = groupName,
            };

            group.SetPosition(new Rect(mousePosition, Vector2.one));

            foreach (GraphElement selectedElement in graphSelection)
            {
                if (!(selectedElement is GraphNode)) continue;

                group.AddElement(selectedElement);
            }

            return group;
        }
        
        public static VisualElement AddClasses(this VisualElement element, params string[] classes)
        {
            foreach (string styleClass in classes)
            {
                element.AddToClassList(styleClass);
            }

            return element;
        }

        public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheetNames)
        {
            foreach (string styleSheet in styleSheetNames)
            {
                StyleSheet stylesheet = (StyleSheet)EditorGUIUtility.Load(styleSheet);
                element.styleSheets.Add(stylesheet);
            }

            return element;
        }

        public static Port CreatePort(this GraphNode node, string portName, PortSettings portSettings)
        {
            Port port = node.InstantiatePort(portSettings.orientation, portSettings.direction, portSettings.capacity, typeof(bool));
            port.portName = portName;

            return port;
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

        public static TextField CreateTextField(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textField = new TextField()
            {
                value = value
            };

            if(onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            return textField;
        }

        public static Label CreateLabel(string value = "")
        {
            Label label = new Label(value);
            return label;
        }

        public static TextField CreateTextArea(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(value, onValueChanged);
            textArea.multiline = true;
            return textArea;
        }

    }

    public struct PortSettings
    {
        public Orientation orientation;
        public Direction direction;
        public Port.Capacity capacity;

        public PortSettings(Orientation ori, Direction dir, Port.Capacity cap)
        {
            orientation = ori;
            direction = dir;
            capacity = cap;
        }
    }

}
