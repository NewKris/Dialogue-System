using System;

namespace VirtualDeviants.Dialogue.SharedNodeData
{
	public class TextData : NodeData
	{
		public string speaker;
		public string text;

		public TextData(string speaker, string text)
		{
			this.speaker = speaker;
			this.text = text;
		}
	}
}