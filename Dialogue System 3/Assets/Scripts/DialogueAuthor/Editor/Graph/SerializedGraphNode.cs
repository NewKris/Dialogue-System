using System;
using UnityEngine;

namespace VirtualDeviants.DialogueAuthor.Editor.Graph {
	[Serializable]
	public struct SerializedGraphNode {
		public NodeTemplate template;
		public int[] connections;
		public Vector2 position;
	}
}