﻿using System;
using UnityEngine;
using VirtualDeviants.Dialogue.RuntimeAsset;

namespace VirtualDeviants.Dialogue.Runtime
{
	public class DialogueInteraction : MonoBehaviour
	{

		public static event Action<DialogueAsset> OnDialogueInteracted;
		
		public DialogueAsset dialogue;

		public void StartDialogue()
		{
			OnDialogueInteracted?.Invoke(dialogue);
		}
	}
}