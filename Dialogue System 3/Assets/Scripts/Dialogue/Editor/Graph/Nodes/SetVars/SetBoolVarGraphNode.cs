using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes.SetVars
{
	public class SetBoolVarGraphNode : GraphNode
	{
		private PopupField<string> _variables;
		private Toggle _newValue;

		private SetBoolVarNode Data => template as SetBoolVarNode;
		
		public SetBoolVarGraphNode(string nodeName, SetBoolVarNode template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData()
		{
			Data.key = _variables.value;
			Data.newValue = _newValue.value;
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();
			AddOutputPort();

			_variables = CreateVariableDropdown(VariableType.BOOLEAN, Data.key);
			_newValue = VisualElementFactory.CreateCheckBox(Data.newValue);

			customDataContainer.AddRange(_variables, _newValue);
			base.Draw(initialPosition);
		}
	}
}