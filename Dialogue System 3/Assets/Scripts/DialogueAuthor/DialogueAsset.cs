using UnityEngine;

namespace VirtualDeviants.DialogueAuthor {
	public class DialogueAsset : ScriptableObject {
		[SerializeReference]
		public NodeTemplate[] nodes;
	}
}
