using System;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.GraphSaving
{
	[Serializable]
	public class SerializedNode
	{
		public string nodeName;
		public string guid;
		public string[] outputGuids;
		public Rect nodeRect;
	}
}