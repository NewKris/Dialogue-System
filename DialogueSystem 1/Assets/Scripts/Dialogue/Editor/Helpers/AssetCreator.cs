using System;
using System.IO;
using UnityEditor;
using VirtualDeviants.Dialogue.Editor.GraphSaving;
using Object = UnityEngine.Object;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class AssetCreator
	{
		public static void CreateAsset(string path, Object newAsset)
		{
			if (File.Exists(path))
				AssetDatabase.DeleteAsset(path);
			
			AssetDatabase.CreateAsset(newAsset, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = newAsset;
		}

	}
}