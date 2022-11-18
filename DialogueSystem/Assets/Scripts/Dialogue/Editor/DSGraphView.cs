using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Nodes;
using VirtualDeviants.Dialogue.Enumerations;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DSGraphView : GraphView
    {
        
        public DSGraphView()
        {
            AddManipulators();
            AddGridBackground();

            AddStyles();
        }

        private DSNode CreateNode(DSNodeType nodeType, Vector2 position)
        {

            DSNode node;

            switch (nodeType)
            {
                case DSNodeType.Text:
                    node = new DSTextNode();
                    break;
                case DSNodeType.Choice:
                    node = new DSChoiceNode();
                    break;
                case DSNodeType.Exit:
                    node = new DSExitNode();
                    break;
                default:
                    node = new DSEntryNode();
                    break;
            }

            node.Initialize(position);
            node.Draw();

            return node;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if(startPort == port 
                || startPort.node == port.node
                || startPort.direction == port.direction) return;

                compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(CreateNodeContextualMenu());
        }

        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(AddMenuEvents);
            return contextualMenuManipulator;
        }

        private void AddMenuEvents(ContextualMenuPopulateEvent menuEvents)
        {
            menuEvents.menu.AppendAction("Create Group", x => AddElement(CreateGroup("Dialogue Group", x.eventInfo.localMousePosition)));
            menuEvents.menu.AppendAction("Add Entry Node", x => AddElement(CreateNode(DSNodeType.Entry, x.eventInfo.localMousePosition)));
            menuEvents.menu.AppendAction("Add Text Node", x => AddElement(CreateNode(DSNodeType.Text, x.eventInfo.localMousePosition)));
            menuEvents.menu.AppendAction("Add Choice Node", x => AddElement(CreateNode(DSNodeType.Choice, x.eventInfo.localMousePosition)));
            menuEvents.menu.AppendAction("Add Exit Node", x => AddElement(CreateNode(DSNodeType.Exit, x.eventInfo.localMousePosition)));
        }

        private Group CreateGroup(string groupName, Vector2 mousePosition)
        {
            Group group = new Group()
            {
                title = groupName,
            };

            group.SetPosition(new Rect(mousePosition, Vector2.one));

            foreach (GraphElement selectedElement in selection)
            {
                if (!(selectedElement is DSNode)) continue;

                group.AddElement(selectedElement);
            }

            return group;
        }

        private void AddStyles()
        {
            this.AddStyleSheets(
                "Dialogue/DSGraphViewStyle.uss", 
                "Dialogue/DSNodeStyles.uss"
            );
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
    }
}
