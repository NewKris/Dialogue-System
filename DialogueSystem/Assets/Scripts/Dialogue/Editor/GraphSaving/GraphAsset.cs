using System.Collections.Generic;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.GraphSaving
{
	public class GraphAsset : ScriptableObject
	{
		public List<SerializedNode> nodes;
		public List<SerializedGroup> groups;
	}
}