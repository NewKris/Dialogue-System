using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Nodes
{
	public class IntVarCheckNode : VarCheckNode<int>
	{
		public int min;
		public int max;
		public bool invert;

		public override bool Evaluate(int value)
		{
			return (value >= min && value <= max) == !invert;
		}
	}
}