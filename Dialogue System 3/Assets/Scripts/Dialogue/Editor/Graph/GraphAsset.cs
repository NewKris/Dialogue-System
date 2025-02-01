using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.Graph
{
	[CreateAssetMenu(menuName = "Dialogue Graph")]
	public class GraphAsset : ScriptableObject
	{
		public SerializedGraphNode[] nodes;
		public SerializedGraphGroup[] groups;
		
		[OnOpenAsset]
		public static bool OnOpenAsset(int instanceID)
		{
			GraphAsset graph = EditorUtility.InstanceIDToObject(instanceID) as GraphAsset;
			
			if (!graph) return false;
				
			DialogueAuthorWindow.OpenWindow(graph);
			return true;
		}
	}
}