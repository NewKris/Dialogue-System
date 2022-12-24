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

        public override void Draw()
        {
            base.Draw();
            AddInputPort();

            titleButtonContainer.AddClasses(ContainerStyle);
        }


    }
}
