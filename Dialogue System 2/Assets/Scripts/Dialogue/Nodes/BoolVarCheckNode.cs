namespace VirtualDeviants.Dialogue.Nodes
{
	public class BoolVarCheckNode : VarCheckNode<bool>
	{
		public override bool Evaluate(bool value)
		{
			return value;
		}
	}
}