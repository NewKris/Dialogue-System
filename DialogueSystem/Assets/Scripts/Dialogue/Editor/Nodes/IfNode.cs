using VirtualDeviants.Dialogue.Core.DialogueVariables;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
	public class IfNode : GraphNode
	{
		public string VariableKey { get; private set; }
		public float ComparisonValue { get; private set; }
		public Comparison ValueComparison { get; private set; }

		public IfNode(
			string variableKey, 
			float comparisonValue, 
			Comparison valueComparison, 
			string nodeName = "If") 
			: base(nodeName)
		{
			
		}
		
	}
}