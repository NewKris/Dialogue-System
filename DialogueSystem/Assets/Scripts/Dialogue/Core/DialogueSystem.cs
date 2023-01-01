using UnityEngine;
using VirtualDeviants.Dialogue.RuntimeAsset;

namespace VirtualDeviants.Dialogue.Core
{
	public class DialogueSystem : MonoBehaviour
	{
		private DialogueUIController _ui;

		private void Awake()
		{
			DialogueInteraction.OnDialogueInteracted += StartDialogue;
		}

		private void OnDestroy()
		{
			DialogueInteraction.OnDialogueInteracted -= StartDialogue;
		}

		private void StartDialogue(DialogueAsset dialogue)
		{
			DialogueAssetReader.StartNewDialogue(dialogue);
		}
		
	}
}