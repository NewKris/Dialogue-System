namespace VirtualDeviants.Dialogue.Nodes
{
	public class StringVarCheckNode : VarCheckNode<string>
	{
		public string expected;

		public override bool Evaluate(string value)
		{
			return string.Equals(value, expected);
		}
	}
}