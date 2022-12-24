using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class GraphTextNode : GraphNode
    {

        private const string TextStyle = "ds-node_text-text";
        private const string NameStyle = "ds-node_text-name";
        private const string TextContainerStyle = "ds-node_text-container";
        private const string ExternalContainerStyle = "ds-node_text-external_container";

        private TextField _speaker;
        private TextField _text;

        public string Speaker => _speaker.value;
        public string Text => _text.value;

        public override void Draw()
        {
            base.Draw();

            AddTitle("Dialogue Text");
            AddInputPort();
            AddOutputPort();

            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddClasses(TextContainerStyle);

            _speaker = ElementUtility.CreateTextField("Speaker");
            _speaker.AddClasses(NameStyle);

            _text = ElementUtility.CreateTextArea("Text");
            _text.AddClasses(TextStyle);

            customDataContainer.Add(_speaker);
            customDataContainer.Add(_text);
            extensionContainer.Add(customDataContainer);
            extensionContainer.AddClasses(ExternalContainerStyle);

            RefreshExpandedState();
        }

    }
}