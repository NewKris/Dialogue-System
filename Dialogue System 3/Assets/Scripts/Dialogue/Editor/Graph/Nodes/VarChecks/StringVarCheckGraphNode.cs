using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes.VarChecks
{
	public class StringVarCheckGraphNode : GraphNode
	{
		private PopupField<string> _variables;
		private TextField _expectedValue;

		private StringVarCheckNode Data => template as StringVarCheckNode;
		
		public StringVarCheckGraphNode(string nodeName, NodeTemplate template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData()
		{
			Data.key = _variables.value;
			Data.expected = _expectedValue.value;
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();
			
			AddOutputPort("True");
			AddOutputPort("False");

			_variables = CreateVariableDropdown(VariableType.STRING, Data.key);
			_expectedValue = VisualElementFactory.CreateTextField(Data.expected);
			
			customDataContainer.AddRange(_variables, _expectedValue);
			base.Draw(initialPosition);
		}
	}
}