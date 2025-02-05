using System;
using UnityEngine;
using VirtualDeviants.DialogueAuthor;

namespace VirtualDeviants.Editor.DialogueAuthor.Graph {
	[Serializable]
	public struct SerializedGraphNode {
		[SerializeReference] 
		public NodeTemplate template;
		
		public int[] connections;
		public Vector2 position;
	}
}