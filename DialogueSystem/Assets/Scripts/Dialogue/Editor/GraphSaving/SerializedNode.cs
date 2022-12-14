using System;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.GraphSaving
{
	[Serializable]
	public class SerializedNode
	{
		public string nodeName;
		public int guid;
		public int[] outputGuids;
		public Rect nodeRect;
	}
}