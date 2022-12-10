using System;

namespace VirtualDeviants.Dialogue.GraphSaving
{
	[Serializable]
	public class TextDialogueNode : DialogueNode
	{
		public string speaker;
		public string text;
	}
}