using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class GraphEntryNode : GraphNode
    {

        private const string ContainerStyle = "ds-node_entry-title_container";
        private const string IdContainerStyle = "ds-node_entry-id_container";
        private const string IdTextStyle = "ds-node_entry-id";

        public override void Draw()
        {
            base.Draw();
            AddOutputPort();

            titleButtonContainer.AddClasses(ContainerStyle);

            VisualElement customContainer = new VisualElement();
            TextField idField = ElementUtility.CreateTextField("ID");
            idField.AddClasses(IdTextStyle);
            customContainer.Add(idField);
            customContainer.AddClasses(IdContainerStyle);

            extensionContainer.Add(customContainer);
        }

    }
}
