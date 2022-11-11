using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Enumerations;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueNode : Node
    {
        public string DialogueName { get; set; }
        public string Text { get; set; }
        public List<string> Choices { get; set; }
        public DialogueType DialogueType { get; set; }

        public void Initialize()
        {
            DialogueName = "DialogueName";
            Choices = new List<string>();
            Text = "Dialogue Text.";
        }

        public void Draw()
        {
            // Title
            TextField dialogueName = new TextField()
            {
                value = DialogueName
            };
            titleContainer.Insert(0, dialogueName);

            // Input Port
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.portName = "In";
            inputContainer.Add(inputPort);

            // Text

            VisualElement customDataContainer = new VisualElement();

            Foldout foldout = new Foldout()
            {
                text = "Dialogue Text"
            };

            TextField text = new TextField()
            {
                value = Text
            };

            foldout.Add(text);
            customDataContainer.Add(foldout);

            extensionContainer.Add(customDataContainer);

            RefreshExpandedState();

        }

    }
}
