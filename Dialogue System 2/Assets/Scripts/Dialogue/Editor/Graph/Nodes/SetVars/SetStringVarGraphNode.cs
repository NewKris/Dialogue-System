using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes.SetVars
{
	public class SetStringVarGraphNode : GraphNode
	{
		private PopupField<string> _variables;
		private TextField _newValue;
		private Toggle _isPlayerInput;

		private SetStringVarNode Data => template as SetStringVarNode;
		
		public SetStringVarGraphNode(string nodeName, SetStringVarNode template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData()
		{
			Data.key = _variables.value;
			Data.newValue = _newValue.value;
			Data.isPlayerInput = _isPlayerInput.value;
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();
			AddOutputPort();

			_variables = CreateVariableDropdown(VariableType.STRING, Data.key);
			_newValue = VisualElementFactory.CreateTextField();
			_isPlayerInput = VisualElementFactory.CreateCheckBox(Data.isPlayerInput, "Is Player Input?");

			customDataContainer.AddRange(_variables, _newValue, _isPlayerInput);
			base.Draw(initialPosition);
		}
	}
}