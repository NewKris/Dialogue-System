using UnityEditor;
using VirtualDeviants.Dialogue.SerializedAsset;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class AssetCreator
	{
		public static void CreateDialogueAsset(string path, DialogueAsset newAsset)
		{
			AssetDatabase.CreateAsset(newAsset, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = newAsset;
		}

		public static void UpdateDialogueAsset(string path, Node[] newNodes)
		{
			DialogueAsset existingAsset = (DialogueAsset) AssetDatabase.LoadAssetAtPath(path, typeof(DialogueAsset));
			existingAsset.nodes = newNodes;
			
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = existingAsset;
		}
		
	}
}