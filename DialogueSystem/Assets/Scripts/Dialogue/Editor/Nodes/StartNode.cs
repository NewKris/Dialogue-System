using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class StartNode : GraphNode
    {

        private const string ContainerStyle = "ds-node_start-title_container";

        public StartNode(string nodeName = "Start") : base(nodeName){}
        
        public override void Draw(Vector2 position)
        {
            base.Draw(position);
            AddOutputPort();

            titleButtonContainer.AddStyleClasses(ContainerStyle);
        }

    }
}
