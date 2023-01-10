using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Core.DialogueVariables;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
	public class VariableNode : GraphNode
	{

		public string VariableKey { get; private set; }
		public float OperationValue { get; private set; }
		public VariableOperation Operation { get; private set; }

		public VariableNode(
			string variableKey = "", 
			float operationValue = 0, 
			VariableOperation operation = VariableOperation.ADD, 
			string nodeName = "Variable")
			: base(nodeName)
		{
			VariableKey = variableKey;
			OperationValue = operationValue;
			Operation = operation;

		}

		public override void Draw(Vector2 position)
		{
			base.Draw(position);
			
			AddInputPort();
			AddOutputPort();

			VisualElement customDataContainer = new VisualElement();

			List<string> keys = DialogueAuthorWindow.variableKeys;
			int index = string.IsNullOrEmpty(VariableKey) ? 0:  keys.IndexOf(VariableKey);
			PopupField<string> popupField = new PopupField<string>(keys, index, OnVariableChanged);
			customDataContainer.Add(popupField);

			EnumField operationEnum = new EnumField(Operation);
			operationEnum.OnValueChanged(OnOperationChanged);
			customDataContainer.Add(operationEnum);

			FloatField valueField = new FloatField()
			{
				value = OperationValue
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

		private void OnOperationChanged(Enum newValue)
		{
			Operation = (VariableOperation)newValue;
		}

		private void OnValueChanged(float newValue)
		{
			OperationValue = newValue;
		}
		
	}
}