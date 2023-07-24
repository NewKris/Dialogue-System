using VirtualDeviants.Dialogue.Core.DialogueVariables;

namespace VirtualDeviants.Dialogue.SharedNodeData
{
	public class IfData : NodeData
	{
		public string key;
		public float value;
		public Comparison comparison;

		public IfData(string key, float value, Comparison comparison)
		{
			this.key = key;
			this.value = value;
			this.comparison = comparison;
		}
	}
}