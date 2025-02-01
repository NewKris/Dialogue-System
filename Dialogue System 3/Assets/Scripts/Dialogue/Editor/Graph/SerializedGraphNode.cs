using System;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.Graph {
	[Serializable]
	public struct SerializedGraphNode {
		[SerializeReference] public NodeTemplate template;
		public int[] connections;
		public Vector2 position;
	}
}