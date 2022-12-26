using System.Collections.Generic;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.GraphSaving
{
	public class GraphAsset : ScriptableObject
	{
		[NonReorderable, SerializeReference]
		public List<SerializedNode> nodes;
		[NonReorderable]
		public List<SerializedGroup> groups;
	}
}