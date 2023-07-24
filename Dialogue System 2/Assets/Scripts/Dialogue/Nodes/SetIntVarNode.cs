using System;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Nodes
{
	public class SetIntVarNode : SetVarNode<int>
	{
		public int operationValue;
		public VariableOperation operation;
		
		public override int GetNewValue(int value)
		{
			switch (operation)
			{
				case VariableOperation.SET:
					return operationValue;
				case VariableOperation.ADD:
					return value + operationValue;
				case VariableOperation.SUBTRACT:
					return value - operationValue;
				case VariableOperation.MULTIPLY:
					return value * operationValue;
				case VariableOperation.DIVIDE:
					return value / operationValue;
				case VariableOperation.MOD:
					return value % operationValue;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}