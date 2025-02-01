using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes.SetVars
{
	public class SetIntVarGraphNode : GraphNode
	{
		private PopupField<string> _variables;
		private EnumField _operation;
		private IntegerField _operationValue;

		private SetIntVarNode Data => template as SetIntVarNode;
		
		public SetIntVarGraphNode(string nodeName, SetIntVarNode template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData()
		{
			Data.key = _variables.value;
			Data.operation = (VariableOperation) _operation.value;
			Data.operationValue = _operationValue.value;
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();
			AddOutputPort();

			_variables = CreateVariableDropdown(VariableType.INTEGER, Data.key);
			_operation = VisualElementFactory.CreateEnumField(Data.operation);
			_operationValue = VisualElementFactory.CreateIntegerField(Data.operationValue);

			customDataContainer.AddRange(_variables, _operation, _operationValue);
			base.Draw(initialPosition);
		}
	}
}