using System;
using UnityEngine;
using VirtualDeviants.Dialogue.SharedNodeData;

namespace VirtualDeviants.Dialogue.Editor.GraphSaving
{
	[Serializable]
	public class SerializedNode
	{
		[SerializeReference]
		public NodeData data;
		public string nodeName;
		public int guid;
		public int[] outputGuids;
		public Vector2 position;
	}
}