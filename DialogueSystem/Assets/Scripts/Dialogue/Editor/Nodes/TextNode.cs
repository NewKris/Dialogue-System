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
        private const string ExternalContainerStyle = "ds-node_text-external_container";

        private readonly string _speaker;
        private readonly string _text;

        private TextField _speakerField;
        private TextField _textField;

        public string Speaker => _speakerField.value;
        public string Text => _textField.value;

        public TextNode(string speaker = "Speaker", string text = "Lorem Ipsum", string nodeName = "Dialogue Node") 
            : base(nodeName)
        {
            _speaker = speaker;
            _text = text;
        }

        public override void Draw(Vector2 position)
        {
            base.Draw(position);

            AddInputPort();
            AddOutputPort();

            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddClasses(TextContainerStyle);

            _speakerField = ElementUtility.CreateTextField(_speaker);
            _speakerField.AddClasses(NameStyle);

            _textField = ElementUtility.CreateTextArea(_text);
            _textField.AddClasses(TextStyle);

            customDataContainer.Add(_speakerField);
            customDataContainer.Add(_textField);
            extensionContainer.Add(customDataContainer);
            extensionContainer.AddClasses(ExternalContainerStyle);

            RefreshExpandedState();
        }

    }
}