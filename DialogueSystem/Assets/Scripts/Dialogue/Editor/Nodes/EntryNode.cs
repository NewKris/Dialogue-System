using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class EntryNode : GraphNode
    {

        private const string ContainerStyle = "ds-node_entry-title_container";
        private const string IdContainerStyle = "ds-node_entry-id_container";
        private const string IdTextStyle = "ds-node_entry-id";

        public EntryNode(string nodeName = "Start") : base(nodeName){}
        
        public override void Draw(Vector2 position)
        {
            base.Draw(position);
            AddOutputPort();

            titleButtonContainer.AddStyleClasses(ContainerStyle);

            VisualElement customContainer = new VisualElement();
            TextField idField = ElementUtility.CreateTextField("ID");
            idField.AddStyleClasses(IdTextStyle);
            customContainer.Add(idField);
            customContainer.AddStyleClasses(IdContainerStyle);

            extensionContainer.Add(customContainer);
        }

    }
}
