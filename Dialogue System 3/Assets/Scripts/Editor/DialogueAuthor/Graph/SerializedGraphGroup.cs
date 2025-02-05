using System;

namespace VirtualDeviants.Editor.DialogueAuthor.Graph {
	[Serializable]
	public struct SerializedGraphGroup {
		public string groupTitle;
		public int[] containedNodes;
	}
}