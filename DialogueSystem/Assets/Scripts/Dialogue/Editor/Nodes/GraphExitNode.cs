using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class GraphExitNode : GraphNode
    {

        private const string ContainerStyle = "ds-node_exit-title_container";

        public GraphExitNode(string nodeName = "Exit") : base(nodeName){}
        
        public override void Draw(Vector2 position)
        {
            base.Draw(position);
            AddInputPort();

            titleButtonContainer.AddClasses(ContainerStyle);
        }


    }
}
