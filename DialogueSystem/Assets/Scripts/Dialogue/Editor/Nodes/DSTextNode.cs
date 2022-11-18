using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSTextNode : DSNode
    {

        private const string textClass = "ds-node-text_text";
        private const string nameClass = "ds-node-text_name";
        private const string textContainerClass = "ds-node-text_container";

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

            // Output Port
            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));
            outputContainer.Add(outputPort);

            // Contents
            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddClasses(textContainerClass);

            TextField speaker = DSElementUtility.CreateTextField("Speaker");
            speaker.AddClasses(nameClass);

            TextField text = DSElementUtility.CreateTextArea("Text");
            text.AddClasses(textClass);

            customDataContainer.Add(speaker);
            customDataContainer.Add(text);
            extensionContainer.Add(customDataContainer);

            RefreshExpandedState();

        }

    }
}
