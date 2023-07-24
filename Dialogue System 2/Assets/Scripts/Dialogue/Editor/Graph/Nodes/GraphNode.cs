using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes
{
	public abstract class GraphNode : Node
	{
		private const string NodeTitleContainerTitle = "node-title-container__title-input";
		private const string NodeCustomDataContainer = "node-custom-data-container";
		private const string NodeHeader = "node-header";
		
		protected readonly VisualElement customDataContainer;
		protected readonly NodeTemplate template;
		private readonly TextField _titleField;

		public string NodeName => _titleField.value;
		public NodeTemplate Template => template;
		
		public Vector2 Position
		{
			get => GetPosition().position;
			private set => SetPosition(new Rect(value, Vector2.one));
		}
		
		protected GraphNode(string nodeName, NodeTemplate template)
		{
			this.template = template;
			
			_titleField = VisualElementFactory.CreateTextField(nodeName);
			_titleField.AddStyleClass(NodeTitleContainerTitle);
			customDataContainer = new VisualElement();
		}

		public abstract void UpdateTemplateData();

		public virtual GraphNode[] GetConnections()
		{
			if (!outputContainer.Children().Any(child => child is Port))
				return Array.Empty<GraphNode>();

			Port[] ports = outputContainer.Children().Where(port => port is Port).Cast<Port>().ToArray();
			return ports.Select(port =>
			{
				if (!port.connections.Any()) return null;
				
				return port.connections.First().input.node as GraphNode;
			}).ToArray();
		}

		public virtual void CreateConnections(DialogueGraphView graphView, params GraphNode[] connections)
		{
			Port[] ports = outputContainer.Children().Where(port => port is Port).Cast<Port>().ToArray();
			
			for (int i = 0; i < connections.Length; i++)
			{
				if(connections[i] == null) continue;
				
				Port inputPort = connections[i].inputContainer.Children().First() as Port;
				graphView.CreateConnection(ports[i], inputPort);
			}
		}

		public virtual void Draw(Vector2 initialPosition)
		{
			VisualElement nodeHeader = new VisualElement();
			nodeHeader.AddStyleClass(NodeHeader);
			contentContainer.Insert(0, nodeHeader);
			
			Position = initialPosition;
			titleContainer.Insert(0, _titleField);
			
			extensionContainer.Add(customDataContainer);
			customDataContainer.AddStyleClass(NodeCustomDataContainer);
			
			RefreshExpandedState();
		}

		protected void AddInputPort()
		{
			Port port = this.CreateInputPort();
			inputContainer.Add(port);
		}

		protected void AddOutputPort(string label = "")
		{
			Port port = this.CreateOutputPort(label);
			outputContainer.Add(port);
		}

		protected PopupField<string> CreateVariableDropdown(VariableType type, string defaultKey)
		{
			List<string> keys = GetVariableKeys(type);
			int defaultIndex = 0;

			if (!string.IsNullOrEmpty(defaultKey))
			{
				defaultIndex = keys.IndexOf(defaultKey);

				if (defaultIndex == -1)
				{
					string message = $"Failed to find key {defaultKey} of type BOOLEAN";
					Debug.LogError(message);
					return VisualElementFactory.CreatePopupField(new List<string>(){$"<failed to find key {defaultKey}>"});
				}
			}
			
			PopupField<string> variables = VisualElementFactory.CreatePopupField(GetVariableKeys(type), defaultIndex);
			return variables;
		}
		
		private List<string> GetVariableKeys(VariableType type)
		{
			if (VariableDatabase.editorInstance == null)
				return new List<string>();

			return VariableDatabase.editorInstance.GetVariableKeys(type);
		}
	}
}