using VirtualDeviants.Dialogue.Core.DialogueVariables;

namespace VirtualDeviants.Dialogue.SharedNodeData
{
	public class VariableData : NodeData
	{
		public string key;
		public float value;
		public VariableOperation operation;

		public VariableData(string key, float value, VariableOperation operation)
		{
			this.key = key;
			this.value = value;
			this.operation = operation;
		}
	}
}