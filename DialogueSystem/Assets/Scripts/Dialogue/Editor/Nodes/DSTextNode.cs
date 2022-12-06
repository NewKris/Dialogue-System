using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSTextNode : DSNode
    {

        private const string textStyle = "ds-node_text-text";
        private const string nameStyle = "ds-node_text-name";
        private const string textContainerStyle = "ds-node_text-container";
        private const string externalContainerStyle = "ds-node_text-external_container";

        public string Speaker { get; set; }
        public string Text { get; set; }

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            NodeName = "Dialogue Text";
            Speaker = "Narrator";
            Text = "Lorem Ipsum";
        }

        public override void Draw()
        {
            base.Draw();

            AddInputPort();
            AddOutputPort();

            // Contents
            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddClasses(textContainerStyle);

            TextField speaker = DSElementUtility.CreateTextField("Speaker");
            speaker.AddClasses(nameStyle);

            TextField text = DSElementUtility.CreateTextArea("Text");
            text.AddClasses(textStyle);

            customDataContainer.Add(speaker);
            customDataContainer.Add(text);
            extensionContainer.Add(customDataContainer);
            extensionContainer.AddClasses(externalContainerStyle);

            RefreshExpandedState();

        }

    }
}
