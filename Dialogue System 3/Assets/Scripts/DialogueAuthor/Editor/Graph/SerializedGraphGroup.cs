using System;

namespace VirtualDeviants.DialogueAuthor.Editor.Graph {
	[Serializable]
	public struct SerializedGraphGroup {
		public string groupTitle;
		public int[] containedNodes;
	}
}