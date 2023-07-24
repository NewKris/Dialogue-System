using UnityEngine;

namespace VirtualDeviants.Dialogue
{
	public class DialogueAsset : ScriptableObject
	{
		[SerializeReference]
		public NodeTemplate[] nodes;
	}
}