using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSEntryNode : DSNode
    {

        private const string entryTitleContainerClass = "ds-node-entry_title-container";
        private const string entryTitleClass = "ds-node-entry_title";

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            NodeName = "Start";
        }

        public override void Draw()
        {
            Label title = DSElementUtility.CreateLabel(NodeName);
            title.AddClasses(entryTitleClass);

            titleContainer.Insert(0, title);
            titleButtonContainer.AddClasses(entryTitleContainerClass);

            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));
            outputContainer.Add(outputPort);
        }

    }
}
