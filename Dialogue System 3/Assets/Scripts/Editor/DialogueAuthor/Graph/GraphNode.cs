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

		private static readonly HashSet<NodeDrawer> Drawers = new HashSet<NodeDrawer>();

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

		public void UpdateTemplateData() {
			// TODO here
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
			AddCustomData(Template);
			
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
		
		private void AddCustomData(NodeTemplate template) {
			_customDataContainer = new VisualElement();
			_customDataContainer.AddStyleClass(CUSTOM_DATA_CONTAINER_CLASS);

			Type drawerType = GetDrawerType(template);

			if (drawerType == null) {
				return;
			}
			
			NodeDrawer drawer = GetDrawer(drawerType);
			_customDataContainer.Add(drawer.Draw(template));

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

		private Type GetDrawerType(NodeTemplate template) {
			return Assembly.GetAssembly(typeof(NodeDrawer))
				.GetTypes()
				.Where(type => Attribute.IsDefined(type, typeof(CustomNodeDrawer)))
				.FirstOrDefault(drawerType => drawerType.GetCustomAttribute<CustomNodeDrawer>()?.type == template.GetType());
		}

		private NodeDrawer GetDrawer(Type drawerType) {
			NodeDrawer drawer = Drawers.FirstOrDefault(d => d.GetType() == drawerType);
			
			if (drawer == null) {
				drawer = CreateDrawerInstance(drawerType);
				Drawers.Add(drawer);
			}

			return drawer;
		}

		private NodeDrawer CreateDrawerInstance(Type drawerType) {
			return Activator.CreateInstance(drawerType) as NodeDrawer;
		}
	}
}