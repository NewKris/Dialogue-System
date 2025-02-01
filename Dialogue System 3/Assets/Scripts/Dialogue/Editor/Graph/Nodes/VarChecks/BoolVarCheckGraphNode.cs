using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes.VarChecks
{
	public class BoolVarCheckGraphNode : GraphNode
	{
		private PopupField<string> _variables;
		
		private BoolVarCheckNode Data => template as BoolVarCheckNode;
		
		public BoolVarCheckGraphNode(string nodeName, NodeTemplate template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData()
		{
			Data.key = _variables.value;
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();
			
			AddOutputPort("True");
			AddOutputPort("False");

			_variables = CreateVariableDropdown(VariableType.BOOLEAN, Data.key);
			customDataContainer.Add(_variables);
			
			base.Draw(initialPosition);
		}
	}
}