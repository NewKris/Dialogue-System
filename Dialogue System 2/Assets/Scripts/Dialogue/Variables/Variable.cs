using System;

namespace VirtualDeviants.Dialogue.Variables
{
	[Serializable]
	public record Variable
	{
		public string key;
		public VariableType type;
		public bool booleanValue;
		public int integerValue;
		public string stringValue;
	}
}