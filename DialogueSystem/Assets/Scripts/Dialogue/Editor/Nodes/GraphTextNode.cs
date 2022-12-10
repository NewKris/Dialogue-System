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

        private const string textStyle = "ds-node_text-text";
        private const string nameStyle = "ds-node_text-name";
        private const string textContainerStyle = "ds-node_text-container";
        private const string externalContainerStyle = "ds-node_text-external_container";

        private TextField _speaker;
        private TextField _text;

        public string Speaker => _speaker.value;
        public string Text => _text.value;

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            NodeName = "Dialogue Text";
        }

        public override void Draw()
        {
            base.Draw();

            AddInputPort();
            AddOutputPort();

            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddClasses(textContainerStyle);

            _speaker = ElementUtility.CreateTextField("Speaker");
            _speaker.AddClasses(nameStyle);

            _text = ElementUtility.CreateTextArea("Text");
            _text.AddClasses(textStyle);

            customDataContainer.Add(_speaker);
            customDataContainer.Add(_text);
            extensionContainer.Add(customDataContainer);
            extensionContainer.AddClasses(externalContainerStyle);

            RefreshExpandedState();
        }

    }
}
