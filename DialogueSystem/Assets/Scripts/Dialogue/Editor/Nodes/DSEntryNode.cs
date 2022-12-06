using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSEntryNode : DSNode
    {

        private const string containerStyle = "ds-node_entry-title_container";
        private const string idContainerStyle = "ds-node_entry-id_container";
        private const string idtextStyle = "ds-node_entry-id";

        public string EntryID { get; set; }

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            NodeName = "Start";
        }

        public override void Draw()
        {
            base.Draw();
            AddOutputPort();

            titleButtonContainer.AddClasses(containerStyle);

            VisualElement customContainer = new VisualElement();
            TextField idField = DSElementUtility.CreateTextField("ID");
            idField.AddClasses(idtextStyle);
            customContainer.Add(idField);
            customContainer.AddClasses(idContainerStyle);

            extensionContainer.Add(customContainer);
        }

    }
}
