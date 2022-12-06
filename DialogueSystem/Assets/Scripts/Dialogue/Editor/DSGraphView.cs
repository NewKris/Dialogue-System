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

        private DialogueAuthorWindow _authorWindow;
        private DSSearchWindow _searchWindow;

        public DSGraphView(DialogueAuthorWindow authorWindow)
        {
            AddSearchWindow();
            AddManipulators();
            AddGridBackground();

            AddStyles();
            _authorWindow = authorWindow;
        }

        private void AddSearchWindow()
        {
            if (_searchWindow == null) 
                _searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();

            _searchWindow.Initialize(this);

            nodeCreationRequest = context => SearchWindow.Open(
                new SearchWindowContext(context.screenMousePosition), 
                _searchWindow);
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(CreateNodeContextualMenu());
        }

        private void AddMenuEvents(ContextualMenuPopulateEvent menuEvents)
        {
            menuEvents.menu.AppendAction("Create Group", x => AddElement(CreateGroup("Dialogue Group", x.eventInfo.localMousePosition)));
            /*menuEvents.menu.AppendAction("Add Entry Node", x => AddElement(CreateNode(DSNodeType.Entry, x.eventInfo.localMousePosition)));
            menuEvents.menu.AppendAction("Add Text Node", x => AddElement(CreateNode(DSNodeType.Text, x.eventInfo.localMousePosition)));
            menuEvents.menu.AppendAction("Add Choice Node", x => AddElement(CreateNode(DSNodeType.Choice, x.eventInfo.localMousePosition)));
            menuEvents.menu.AppendAction("Add Exit Node", x => AddElement(CreateNode(DSNodeType.Exit, x.eventInfo.localMousePosition)));*/
        }

        public DSNode CreateNode(DSNodeType nodeType, Vector2 mousePosition, bool searchWindow = false)
        {
            mousePosition = GetLocalMousePosition(mousePosition, searchWindow);

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

            node.Initialize(mousePosition);
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

        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(AddMenuEvents);
            return contextualMenuManipulator;
        }

        private Group CreateGroup(string groupName, Vector2 mousePosition)
        {

            mousePosition = GetLocalMousePosition(mousePosition);

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

        public Vector2 GetLocalMousePosition(Vector2 mouseScreenPos, bool isSearchWindow = false)
        {

            if (isSearchWindow)
                mouseScreenPos -= _authorWindow.position.position;

            Vector2 localPosition = contentViewContainer.WorldToLocal(mouseScreenPos);

            return localPosition;
        }

    }
}
