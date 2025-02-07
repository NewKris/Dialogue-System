using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor;
using VirtualDeviants.DialogueAuthor.MemberAttributes;
using VirtualDeviants.DialogueAuthor.NodeAttributes;
using VirtualDeviants.Editor.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.NodeDrawers;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Graph {
	public class GraphNode : Node {
		private const string TITLE_CONTAINER_TITLE_CLASS = "title-container__title";
		private const string TITLE_CONTAINER_CLASS = "title-container";
		private const string CUSTOM_DATA_CONTAINER_CLASS = "custom-data-container";

		private static readonly HashSet<NodeMemberDrawer> Drawers = new HashSet<NodeMemberDrawer>();

		private readonly List<Port> _outputPorts;
		
		private Port _inputPort;
		private VisualElement _customDataContainer;
		private Label _title;

		public NodeTemplate Template { get; }

		public Vector2 Position {
			get => GetPosition().position;
			private set => SetPosition(new Rect(value, Vector2.one));
		}
		
		public GraphNode(NodeTemplate template) {
			Template = template;
			_outputPorts = new List<Port>();
		}

		public GraphNode[] GetConnections() {
			return GetOutputPorts().Select(port => {
				if (!port.connections.Any()) {
					return null;
				}
				
				return port.connections.First().input.node as GraphNode;
			}).ToArray();
		}

		public Port GetInputPort() {
			return _inputPort;
		}

		public List<Port> GetOutputPorts() {
			return _outputPorts;
		}

		public void Draw(Vector2 initialPosition) {
			Position = initialPosition;

			AddTitle();
			AddDefaultPorts();
			DrawCustomData(Template);
			
			RefreshExpandedState();
		}
		
		public Port CreateOutputPort() {
			Port newPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
			newPort.portName = "";
			_outputPorts.Add(newPort);

			return newPort;
		}

		public void DeletePort(Port port) {
			Edge[] connections = port.connections.ToArray();
			
			foreach (Edge connection in connections) {
				port.Disconnect(connection);
				connection.input.Disconnect(connection);
				connection.RemoveFromHierarchy();
			}
			
			_outputPorts.Remove(port);
		}

		private void AddTitle() {
			_title = VisualElementFactory.CreateLabel(GetNodeTitle());
			_title.AddStyleClass(TITLE_CONTAINER_TITLE_CLASS);
			
			titleContainer.Insert(0, _title);
			titleContainer.AddStyleClass(TITLE_CONTAINER_CLASS);
		}

		private void AddDefaultPorts() {
			if (ShouldCreateInputPort()) {
				inputContainer.Add(CreateInputPort());
			}
			
			if (ShouldCreateOutputPort()) {
				outputContainer.Add(CreateOutputPort());
			}
		}
		
		private void DrawCustomData(NodeTemplate template) {
			_customDataContainer = new VisualElement();
			_customDataContainer.AddStyleClass(CUSTOM_DATA_CONTAINER_CLASS);

			FieldInfo[] fields = template.GetFieldsWithAttribute(typeof(NodeMember));
			
			foreach (FieldInfo fieldInfo in fields) {
				NodeMember attribute = fieldInfo.GetCustomAttribute<NodeMember>();
				Type drawerType = FindMemberDrawerType(attribute);

				if (drawerType == null) {
					continue;
				}

				NodeMemberDrawer drawer = GetDrawer(drawerType);
				_customDataContainer.Add(drawer.Draw(fieldInfo, this, template));
			}

			extensionContainer.Add(_customDataContainer);
		}
        
		private bool ShouldCreateOutputPort() {
			return Template.GetAttribute<RemoveDefaultOutputPort>() == null;
		}
		
		private bool ShouldCreateInputPort() {
			return Template.GetAttribute<RemoveDefaultInputPort>() == null;
		}

		private string GetNodeTitle() {
			return Template.GetAttribute<NodeTitle>()?.title ?? Template.ToString();
		}

		private Type FindMemberDrawerType(NodeMember nodeMember) {
			return Assembly.GetAssembly(typeof(NodeMemberDrawer))
				.GetTypes()
				.Where(type => Attribute.IsDefined(type, typeof(CustomMemberDrawer)))
				.FirstOrDefault(drawerType => drawerType.GetCustomAttribute<CustomMemberDrawer>().type == nodeMember.GetType());
		}

		private NodeMemberDrawer GetDrawer(Type drawerType) {
			NodeMemberDrawer drawer = Drawers.FirstOrDefault(d => d.GetType() == drawerType);
			
			if (drawer == null) {
				drawer = CreateDrawerInstance(drawerType);
				Drawers.Add(drawer);
			}

			return drawer;
		}

		private NodeMemberDrawer CreateDrawerInstance(Type drawerType) {
			return Activator.CreateInstance(drawerType) as NodeMemberDrawer;
		}
		
		private Port CreateInputPort() {
			_inputPort = InstantiatePort(
				Orientation.Horizontal, 
				Direction.Input,
				Port.Capacity.Multi,
				typeof(bool)
			);

			_inputPort.portName = "";

			return _inputPort;
		}
	}
}