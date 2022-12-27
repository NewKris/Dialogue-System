using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;
using VirtualDeviants.Dialogue.Editor.Nodes;
using VirtualDeviants.Dialogue.Enumerations;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueGraphView : GraphView
    {

        private DialogueAuthorWindow _authorWindow;
        private DialogueGraphSearchWindow _searchWindow;

        public DialogueAuthorWindow AuthorWindow
        {
            set => _authorWindow = value;
        }
        
        public DialogueGraphView()
        {
            AddSearchWindow();
            AddManipulators();
            AddGridBackground();

            AddStyles();
        }

        private void AddSearchWindow()
        {
            if (_searchWindow == null) 
                _searchWindow = ScriptableObject.CreateInstance<DialogueGraphSearchWindow>();

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
            menuEvents.menu.AppendAction("Create Group", x => 
                AddElement(ElementUtility.CreateGroup("Dialogue Group", selection))
            );
        }

        public GraphNode CreateNode(NodeType nodeType, Vector2 mousePosition, bool searchWindow = false)
        {
            mousePosition = GetLocalMousePosition(mousePosition, searchWindow);

            GraphNode node;

            switch (nodeType)
            {
                case NodeType.TEXT:
                    node = new GraphTextNode();
                    break;
                case NodeType.CHOICE:
                    node = new GraphChoiceNode();
                    break;
                case NodeType.EXIT:
                    node = new GraphExitNode();
                    break;
                default:
                    node = new GraphEntryNode();
                    break;
            }

            node.Draw(mousePosition);

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

        public void CreateConnection(Port from, Port to)
        {
            Edge edge = from.ConnectTo(to);
            AddElement(edge);
        }

        public void LoadGroup(string groupName, GraphNode[] children)
        {
            selection.Clear();
            foreach (GraphNode child in children)
                selection.Add(child);
            
            AddElement(ElementUtility.CreateGroup(groupName, selection));
        }

        public void AlignHorizontal()
        {
            List<GraphNode> nodesToAlign = selection
                .Where(node => node is GraphNode)
                .Cast<GraphNode>()
                .OrderBy(node => node.Position.x)
                .ToList();

            if(nodesToAlign.Count == 0) return;

            Vector2 pivot = nodesToAlign[0].Position;
            
            foreach (GraphNode node in nodesToAlign)
            {
                node.Position = pivot;
                pivot.x += node.contentRect.width;
            }
        }

        public void AlignVertical()
        {
            List<GraphNode> nodesToAlign = selection
                .Where(node => node is GraphNode)
                .Cast<GraphNode>()
                .OrderBy(node => node.Position.y)
                .ToList();

            if(nodesToAlign.Count == 0) return;

            Vector2 pivot = nodesToAlign[0].Position;
            
            foreach (GraphNode node in nodesToAlign)
            {
                node.Position = pivot;
                pivot.y += node.contentRect.height;
            }
        }

        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(AddMenuEvents);
            return contextualMenuManipulator;
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

        private Vector2 GetLocalMousePosition(Vector2 mouseScreenPos, bool isSearchWindow = false)
        {

            if (isSearchWindow)
                mouseScreenPos -= _authorWindow.position.position;

            Vector2 localPosition = contentViewContainer.WorldToLocal(mouseScreenPos);

            return localPosition;
        }
    }
}
