using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSExitNode : DSNode
    {

        private const string containerStyle = "ds-node_exit-title_container";

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            NodeName = "Exit";
        }

        public override void Draw()
        {
            base.Draw();
            AddInputPort();

            titleButtonContainer.AddClasses(containerStyle);
        }


    }
}
