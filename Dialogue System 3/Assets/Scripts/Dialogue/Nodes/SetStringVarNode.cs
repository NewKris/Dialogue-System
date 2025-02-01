namespace VirtualDeviants.Dialogue.Nodes
{
	public class SetStringVarNode : SetVarNode<string>
	{
		public string newValue;
		public bool isPlayerInput;
		
		public override string GetNewValue(string value)
		{
			return newValue;
		}
	}
}