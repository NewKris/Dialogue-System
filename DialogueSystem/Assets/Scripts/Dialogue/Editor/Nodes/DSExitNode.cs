using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSExitNode : DSNode
    {

        private const string exitTitleContainerClass = "ds-node-exit_title-container";
        private const string exitTitleClass = "ds-node-exit_title";


        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            NodeName = "Exit";
        }

        public override void Draw()
        {
            Label title = DSElementUtility.CreateLabel(NodeName);
            title.AddClasses(exitTitleClass);

            titleContainer.Insert(0, title);
            titleButtonContainer.AddClasses(exitTitleContainerClass);

            Port inputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi));
            inputContainer.Add(inputPort);
        }


    }
}
