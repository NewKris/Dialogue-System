namespace VirtualDeviants.Dialogue.Nodes
{
	public class SetBoolVarNode : SetVarNode<bool>
	{
		public bool newValue;
		
		public override bool GetNewValue(bool value)
		{
			return newValue;
		}
	}
}