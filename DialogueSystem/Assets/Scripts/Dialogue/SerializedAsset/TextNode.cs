using System;

namespace VirtualDeviants.Dialogue.SerializedAsset
{
	[Serializable]
	public class TextNode : Node
	{
		public string speaker;
		public string text;
	}
}