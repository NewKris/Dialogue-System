using System;
using UnityEngine;
using VirtualDeviants.Dialogue.SharedNodeData;

namespace VirtualDeviants.Dialogue.RuntimeAsset
{
	[Serializable]
	public class DialogueNode
	{
		[SerializeReference]
		public NodeData data;
		public int guid;
		public int[] outputGuids;
	}
}