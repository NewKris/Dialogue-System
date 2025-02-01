using System;

namespace VirtualDeviants.Dialogue.Editor.Graph {
	[Serializable]
	public struct SerializedGraphGroup {
		public string groupTitle;
		public int[] containedNodes;
	}
}