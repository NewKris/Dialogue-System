using System.Collections.Generic;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.GraphSaving
{
	[CreateAssetMenu(menuName = "Dialogue Graph", fileName = "New Graph")]
	public class GraphAsset : ScriptableObject
	{
		[NonReorderable, SerializeReference]
		public List<SerializedNode> nodes;
		[NonReorderable]
		public List<SerializedGroup> groups;
	}
}