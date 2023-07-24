namespace VirtualDeviants.Dialogue.Nodes
{
	public abstract class VarCheckNode<T> : NodeTemplate
	{
		public string key;

		public abstract bool Evaluate(T value);
	}
}