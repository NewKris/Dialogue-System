using System;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.Graph
{
	[Serializable]
	public struct SerializedGraphNode
	{
		[HideInInspector] public string nodeTitle;
		[SerializeReference]
		public NodeTemplate template;
		public int[] connections;
		public Vector2 position;
	}
}