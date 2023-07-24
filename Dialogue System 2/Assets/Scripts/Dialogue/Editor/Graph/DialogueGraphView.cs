using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Graph.Nodes;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.Graph
{
	public class DialogueGraphView : GraphView
	{
		private NodeSearchWindow _searchWindow;
		private readonly DialogueAuthorWindow _authorWindow;
		
		public DialogueGraphView(DialogueAuthorWindow authorWindow)
		{
			_authorWindow = authorWindow;
			CreateGrid();
			AddSearchWindow();
			AddManipulators();
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
			graphViewChanged = OnGraphChanged;
		}

		public void AddNodeToGraph(GraphNode graphNode, Vector2 position, bool convertToLocalPosition = false)
		{
			if(convertToLocalPosition)
				position = ToLocalMousePosition(position);
			
			AddElement(graphNode);
			graphNode.Draw(position);
		}

		public void AddGroupToGraph(string groupTitle, GraphNode[] children)
		{
			selection = children.Cast<ISelectable>().ToList();
			AddElement(VisualElementFactory.CreateGroup(groupTitle, selection));
		}

		public void CreateConnection(Port from, Port to)
		{
			AddElement(from.ConnectTo(to));
		}
		
		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
		{
			return ports.Where(port => 
				startPort != port 
				&& startPort.node != port.node 
				&& startPort.direction != port.direction
			).ToList();
		}

		private void CreateGrid()
		{
			this.LoadStyleSheet(StyleBlackBoard.GridStyleSheets);
			Insert(0, VisualElementFactory.CreateGridBackground());			
		}
		
		private void AddManipulators()
		{
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());
			this.AddManipulator(CreateContextMenu());
		}

		private void AddSearchWindow()
		{
			if (_searchWindow == null)
				_searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
			
			_searchWindow.Initialize(this);
			
			nodeCreationRequest = (context) => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
		}

		private Vector2 ToLocalMousePosition(Vector2 mousePosition)
		{
			mousePosition -= _authorWindow.position.position;
			mousePosition = contentViewContainer.WorldToLocal(mousePosition); 
			return mousePosition;
		}

		private IManipulator CreateContextMenu()
		{
			ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(CreateMenuEvents);
			return contextualMenuManipulator;
		}

		private void CreateMenuEvents(ContextualMenuPopulateEvent menuEvents)
		{
			menuEvents.menu.AppendAction("Create Group", x =>
				AddElement(VisualElementFactory.CreateGroup("Node Group", selection))
			);
		}

		private GraphViewChange OnGraphChanged(GraphViewChange change)
		{
			_authorWindow.WarnForUnsavedChanges();
			return change;
		}
	}
}