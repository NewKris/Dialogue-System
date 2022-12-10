using System;

namespace VirtualDeviants.Dialogue.SerializedAsset
{
	[Serializable]
	public class Node
	{
		public string nodeName;
		public string guid;
		public string[] outputGuids;
	}
}