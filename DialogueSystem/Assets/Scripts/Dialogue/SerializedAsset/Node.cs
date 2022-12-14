using System;

namespace VirtualDeviants.Dialogue.SerializedAsset
{
	[Serializable]
	public class Node
	{
		public string nodeName;
		public int guid;
		public int[] outputGuids;
	}
}