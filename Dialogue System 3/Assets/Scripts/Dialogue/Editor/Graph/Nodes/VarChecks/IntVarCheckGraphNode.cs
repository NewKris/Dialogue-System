using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes.VarChecks
{
	public class IntVarCheckGraphNode : GraphNode
	{
		private PopupField<string> _variables;
		private IntegerField _minValue;
		private IntegerField _maxValue;
		private Toggle _invert;

		private IntVarCheckNode Data => template as IntVarCheckNode;
		
		public IntVarCheckGraphNode(string nodeName, IntVarCheckNode template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData()
		{
			Data.key = _variables.value;
			Data.min = _minValue.value;
			Data.max = _maxValue.value;
			Data.invert = _invert.value;
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();
			AddOutputPort("True");
			AddOutputPort("False");

			_variables = CreateVariableDropdown(VariableType.INTEGER, Data.key);
			_minValue = VisualElementFactory.CreateIntegerField(Data.min);
			_maxValue = VisualElementFactory.CreateIntegerField(Data.max);
			_invert = VisualElementFactory.CreateCheckBox(Data.invert, "Invert: ");

			customDataContainer.AddRange(_variables, _minValue, _maxValue, _invert);
			base.Draw(initialPosition);
		}
	}
}