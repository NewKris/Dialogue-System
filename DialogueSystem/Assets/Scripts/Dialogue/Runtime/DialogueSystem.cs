using System;
using UnityEngine;
using VirtualDeviants.Dialogue.RuntimeAsset;

namespace VirtualDeviants.Dialogue.Runtime
{
	public class DialogueSystem : MonoBehaviour
	{
		private DialogueUIController _ui;
		private DialogueAssetReader _reader;

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
			_reader.StartNewDialogue(dialogue);
		}
		
	}
}