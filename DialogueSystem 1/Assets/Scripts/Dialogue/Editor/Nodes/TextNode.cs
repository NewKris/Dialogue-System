using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class TextNode : GraphNode
    {

        private const string TextStyle = "ds-node_text-text";
        private const string NameStyle = "ds-node_text-name";
        private const string TextContainerStyle = "ds-node_text-container";

        public string Speaker { get; private set; }
        public string Text { get; private set; }

        public TextNode(string speaker = "Speaker", string text = "Lorem Ipsum", string nodeName = "Dialogue Node") 
            : base(nodeName)
        {
            Speaker = speaker;
            Text = text;
        }

        public override void Draw(Vector2 position)
        {
            base.Draw(position);

            AddInputPort();
            AddOutputPort();

            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddStyleClasses(TextContainerStyle);

            TextField speakerField = ElementUtility.CreateTextField(Speaker);
            speakerField.AddStyleClasses(NameStyle);
            speakerField.OnValueChanged(UpdateSpeaker);

            TextField textField = ElementUtility.CreateTextArea(Text);
            textField.AddStyleClasses(TextStyle);
            textField.OnValueChanged(UpdateText);

            customDataContainer.Add(speakerField);
            customDataContainer.Add(textField);
            
            extensionContainer.Add(customDataContainer);
            RefreshExpandedState();
        }

        private void UpdateSpeaker(string speaker)
        {
            Speaker = speaker;
        }

        private void UpdateText(string text)
        {
            Text = text;
        }

    }
}