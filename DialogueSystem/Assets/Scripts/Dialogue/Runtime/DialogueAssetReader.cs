using VirtualDeviants.Dialogue.RuntimeAsset;

namespace VirtualDeviants.Dialogue.Runtime
{
	public class DialogueAssetReader
	{
		public DialogueAsset dialogue;

		public void StartNewDialogue(DialogueAsset newDialogue)
		{
			dialogue = newDialogue;
		}
		
		public DialogueNode Next()
		{
			return null;
		}
		
	}
}