namespace VirtualDeviants.Dialogue.Nodes
{
	public abstract class SetVarNode<T> : NodeTemplate
	{
		public string key;

		public abstract T GetNewValue(T value);
	}
}