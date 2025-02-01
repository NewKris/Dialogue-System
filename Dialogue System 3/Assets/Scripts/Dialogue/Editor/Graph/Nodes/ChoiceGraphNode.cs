using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes
{
	public class ChoiceGraphNode : GraphNode
	{
		private const string ChoiceNodeOutputContainer = "choice-node__output-container";
		private const string ChoiceNodeInputField = "choice-node__input-field";
		private const string ChoiceNodeDeleteButton = "choice-node__delete-button";
		private const string ChoiceNodeAddButton = "choice-node__add-button";
		private const string ChoiceNodePortRow = "choice-node__port-row";

		private readonly Dictionary<TextField, Port> _choices;
		private ChoiceNode Data => template as ChoiceNode;
		
		public ChoiceGraphNode(string nodeName, ChoiceNode template) : base(nodeName, template)
		{
			_choices = new Dictionary<TextField, Port>();
		}
		
		public override void UpdateTemplateData()
		{
			Data.choices = _choices.Keys.Select(key => key.value).ToList();
		}

		public override GraphNode[] GetConnections()
		{
			return _choices.Values.Select(port =>
			{
				if (!port.connections.Any()) return null;
				
				return (GraphNode) port.connections.First().input.node;
			}).ToArray();
		}

		public override void CreateConnections(DialogueGraphView graphView, params GraphNode[] connections)
		{
			Port[] ports = _choices.Values.ToArray();

			for (int i = 0; i < connections.Length; i++)
			{
				if(connections[i] == null) continue;
				
				Port inputPort = connections[i].inputContainer.Children().First() as Port;
				graphView.CreateConnection(ports[i], inputPort);
			}
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();

			Button addChoice = VisualElementFactory.CreateButton("+", AddChoice);
			addChoice.AddStyleClass(ChoiceNodeAddButton);
			customDataContainer.Add(addChoice);
			
			foreach (string choice in Data.choices)
			{
				outputContainer.Add(CreateChoicePort(choice));
			}
			
			outputContainer.AddStyleClass(ChoiceNodeOutputContainer);
			
			base.Draw(initialPosition);
		}

		private void AddChoice()
		{
			const string newChoice = "New Choice";
			outputContainer.Add(CreateChoicePort(newChoice));
			RefreshExpandedState();
		}

		private VisualElement CreateChoicePort(string choice)
		{
			VisualElement portRow = new VisualElement();
			
			Port outputPort = this.CreateOutputPort();

			TextField choiceText = VisualElementFactory.CreateTextField(choice);
			choiceText.AddStyleClass(ChoiceNodeInputField);
			
			_choices.Add(choiceText, outputPort);
			
			Button deleteChoice = VisualElementFactory.CreateButton("x", () => DeleteChoice(portRow, choiceText, outputPort));
			deleteChoice.AddStyleClass(ChoiceNodeDeleteButton);
			
			portRow.AddRange(outputPort, choiceText, deleteChoice);
			portRow.AddStyleClass(ChoiceNodePortRow);
			
			return portRow;
		}

		private void DeleteChoice(VisualElement row, TextField choiceText, Port port)
		{
			if(_choices.Count <= 1) return;

			_choices.Remove(choiceText);
			
			if (port.connected)
			{
				Edge edge = port.connections.First();
				edge.input.Disconnect(edge);
				edge.RemoveFromHierarchy();
			}
			
			outputContainer.Remove(row);
		}
	}
}