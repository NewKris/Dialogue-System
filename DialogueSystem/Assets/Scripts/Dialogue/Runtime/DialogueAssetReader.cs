using VirtualDeviants.Dialogue.SerializedAsset;

namespace VirtualDeviants.Dialogue.Runtime
{
	public class DialogueAssetReader
	{
		public DialogueAsset dialogue;

		public void StartNewDialogue(DialogueAsset newDialogue)
		{
			dialogue = newDialogue;
		}
		
		public Node Next()
		{
			return null;
		}
		
	}
}