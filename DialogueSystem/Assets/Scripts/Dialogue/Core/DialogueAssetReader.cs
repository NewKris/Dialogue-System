using VirtualDeviants.Dialogue.RuntimeAsset;

namespace VirtualDeviants.Dialogue.Core
{
	public static class DialogueAssetReader
	{
		public static DialogueAsset dialogue;

		public static void StartNewDialogue(DialogueAsset newDialogue)
		{
			dialogue = newDialogue;
		}

	}
}