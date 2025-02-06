using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor;
using VirtualDeviants.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.NodeDrawers;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Graph {
	public class GraphNode : Node {
		private const string TITLE_CONTAINER_TITLE_CLASS = "title-container__title";
		private const string TITLE_CONTAINER_CLASS = "title-container";
		private const string CUSTOM_DATA_CONTAINER_CLASS = "custom-data-container";

		private static readonly HashSet<NodeMemberDrawer> Drawers = new HashSet<NodeMemberDrawer>();

		private VisualElement _customDataContainer;
		private Label _title;

		public NodeTemplate Template { get; }

		public Vector2 Position {
			get => GetPosition().position;
			private set => SetPosition(new Rect(value, Vector2.one));
		}
		
		public GraphNode(NodeTemplate template) {
			Template = template;
		}

		public GraphNode[] GetConnections() {
			if (!outputContainer.Children().Any(child => child is Port)) {
				return Array.Empty<GraphNode>();
			}

			Port[] ports = outputContainer.Children().Where(port => port is Port).Cast<Port>().ToArray();
			
			return ports.Select(port => {
				if (!port.connections.Any()) {
					return null;
				}
				
				return port.connections.First().input.node as GraphNode;
			}).ToArray();
		}

		public Port GetInputPort() {
			return inputContainer.Children().First() as Port;;
		}

		public Port[] GetOutputPorts() {
			return outputContainer.Children().Where(port => port is Port).Cast<Port>().ToArray();
		}

		public void Draw(Vector2 initialPosition) {
			Position = initialPosition;

			AddTitle();
			AddDefaultPorts();
			DrawCustomData(Template);
			
			RefreshExpandedState();
		}

		private void AddTitle() {
			_title = VisualElementFactory.CreateLabel(GetNodeTitle());
			_title.AddStyleClass(TITLE_CONTAINER_TITLE_CLASS);
			
			titleContainer.Insert(0, _title);
			titleContainer.AddStyleClass(TITLE_CONTAINER_CLASS);
		}

		private void AddDefaultPorts() {
			if (ShouldCreateInputPort()) {
				inputContainer.Add(VisualElementFactory.CreateInputPort(this));
			}
			
			if (ShouldCreateOutputPort()) {
				outputContainer.Add(VisualElementFactory.CreateOutputPort(this));
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
				_customDataContainer.Add(drawer.Draw(fieldInfo, template));
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
	}
}