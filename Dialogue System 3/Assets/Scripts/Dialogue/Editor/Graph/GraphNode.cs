using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.Graph {
	public class GraphNode : Node {
		private const string NODE_TITLE_CONTAINER_TITLE = "node-title-container__title-input";
		private const string NODE_CUSTOM_DATA_CONTAINER = "node-custom-data-container";
		private const string NODE_HEADER = "node-header";
		
		private readonly VisualElement _customDataContainer;
		private readonly Label _title;

		public NodeTemplate Template { get; }

		public Vector2 Position {
			get => GetPosition().position;
			private set => SetPosition(new Rect(value, Vector2.one));
		}
		
		public GraphNode(NodeTemplate template) {
			Template = template;
			
			_title = VisualElementFactory.CreateLabel(GetNodeTitle());
			_title.AddStyleClass(NODE_TITLE_CONTAINER_TITLE);
			
			_customDataContainer = new VisualElement();
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
			VisualElement nodeHeader = new VisualElement();
			nodeHeader.AddStyleClass(NODE_HEADER);
			contentContainer.Insert(0, nodeHeader);
			
			Position = initialPosition;
			titleContainer.Insert(0, _title);
			
			extensionContainer.Add(_customDataContainer);
			_customDataContainer.AddStyleClass(NODE_CUSTOM_DATA_CONTAINER);
			
			RefreshExpandedState();
		}

		private string GetNodeTitle() {
			return Template.GetType().ToString();
		}
	}
}