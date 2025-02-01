using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.Graph {
	public class DialogueGraphView : GraphView {
		private NodeSearchWindow _searchWindow;
		private EditorWindow _window;
		
		public DialogueGraphView(EditorWindow window) {
			_window = window;
			
			CreateGrid();
			AddSearchWindow();
			AddManipulators();
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
			
			graphViewChanged = changes => {
				WarnUnchangedChanges.Invoke();
				return changes;
			};
		}

		public void AddNodeToGraph(GraphNode graphNode, Vector2 position, bool convertToLocalPosition = false) {
			if (convertToLocalPosition) {
				position = ToLocalMousePosition(position);
			}
			
			AddElement(graphNode);
			graphNode.Draw(position);
		}

		public void AddGroupToGraph(string groupTitle, GraphNode[] children) {
			selection = children.Cast<ISelectable>().ToList();
			AddElement(VisualElementFactory.CreateGroup(groupTitle, selection));
		}
		
		public void CreateConnection(Port from, Port to) {
			AddElement(from.ConnectTo(to));
		}

		private void CreateGrid() {
			this.LoadStyleSheet(StyleBlackBoard.GridStyleSheets);
			Insert(0, VisualElementFactory.CreateGridBackground());			
		}
		
		private void AddManipulators() {
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());
			this.AddManipulator(CreateContextMenu());
		}

		private void AddSearchWindow() {
			if (_searchWindow == null) {
				_searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
			}
			
			_searchWindow.Initialize(this);
			
			nodeCreationRequest = (context) => SearchWindow.Open(
				new SearchWindowContext(context.screenMousePosition, 100, 100),
				_searchWindow
			);
		}

		private Vector2 ToLocalMousePosition(Vector2 mousePosition) {
			mousePosition -= _window.position.position;
			mousePosition = contentViewContainer.WorldToLocal(mousePosition); 
			return mousePosition;
		}

		private IManipulator CreateContextMenu() {
			ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(CreateMenuEvents);
			return contextualMenuManipulator;
		}

		private void CreateMenuEvents(ContextualMenuPopulateEvent menuEvents) {
			menuEvents.menu.AppendAction("Create Group", x =>
				AddElement(VisualElementFactory.CreateGroup("Node Group", selection))
			);
		}
	}
}