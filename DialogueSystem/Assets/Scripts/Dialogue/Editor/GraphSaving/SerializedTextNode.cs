using System;

namespace VirtualDeviants.Dialogue.Editor.GraphSaving
{
	[Serializable]
	public class SerializedTextNode : SerializedNode
	{
		public string speaker;
		public string text;
	}
}