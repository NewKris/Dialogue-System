using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor.Attributes;
using VirtualDeviants.DialogueAuthor.Editor.Utility;

namespace VirtualDeviants.DialogueAuthor.Editor.Graph {
	public class GraphNode : Node {
		private const string TITLE_CONTAINER_TITLE_CLASS = "container__title";
		private const string TITLE_CONTAINER_CLASS = "title-container";
		private const string CUSTOM_DATA_CONTAINER_CLASS = "custom-data-container";
		
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
			_title.AddStyleClass(TITLE_CONTAINER_TITLE_CLASS);
			
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
			Position = initialPosition;
			
			titleContainer.Insert(0, _title);
			titleContainer.AddStyleClass(TITLE_CONTAINER_CLASS);
			
			extensionContainer.Add(_customDataContainer);
			_customDataContainer.AddStyleClass(CUSTOM_DATA_CONTAINER_CLASS);

			if (ShouldCreateInputPort()) {
				inputContainer.Add(VisualElementFactory.CreateInputPort(this));
			}
			
			if (ShouldCreateOutputPort()) {
				outputContainer.Add(VisualElementFactory.CreateOutputPort(this));
			}
			
			RefreshExpandedState();
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
	}
}