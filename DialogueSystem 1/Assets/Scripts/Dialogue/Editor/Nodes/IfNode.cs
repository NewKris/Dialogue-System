using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Core.DialogueVariables;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
	public class IfNode : GraphNode
	{
		public string VariableKey { get; private set; }
		public float ComparisonValue { get; private set; }
		public Comparison ValueComparison { get; private set; }

		public IfNode(
			string variableKey = "", 
			float comparisonValue = 0, 
			Comparison valueComparison = Comparison.LESS, 
			string nodeName = "If") 
			: base(nodeName)
		{
			VariableKey = variableKey;
			ComparisonValue = comparisonValue;
			ValueComparison = valueComparison;
		}

		public override void Draw(Vector2 position)
		{
			base.Draw(position);
			
			AddInputPort();

			outputContainer.Add(this.CreatePort("True", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single)));
			outputContainer.Add(this.CreatePort("False", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single)));

			VisualElement customDataContainer = new VisualElement();
			
			List<string> keys = DialogueAuthorWindow.variableKeys;
			int index = string.IsNullOrEmpty(VariableKey) ? 0:  keys.IndexOf(VariableKey);
			PopupField<string> popupField = new PopupField<string>(keys, index, OnVariableChanged);
			customDataContainer.Add(popupField);
			
			EnumField operationEnum = new EnumField(ValueComparison);
			operationEnum.OnValueChanged(OnComparisonChanged);
			customDataContainer.Add(operationEnum);
			
			FloatField valueField = new FloatField()
			{
				value = ComparisonValue
			};
			customDataContainer.Add(valueField);
			
			extensionContainer.Add(customDataContainer);
			RefreshExpandedState();
		}
		
		private string OnVariableChanged(string arg)
		{
			VariableKey = arg;
			return arg;
		}

		private void OnComparisonChanged(Enum newValue)
		{
			ValueComparison = (Comparison)newValue;
		}

		private void OnValueChanged(float newValue)
		{
			ComparisonValue = newValue;
		}
		
	}
}