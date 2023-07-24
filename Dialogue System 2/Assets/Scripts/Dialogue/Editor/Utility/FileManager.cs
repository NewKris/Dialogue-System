using System;
using UnityEditor;
using VirtualDeviants.Dialogue.Editor.Graph;

namespace VirtualDeviants.Dialogue.Editor.Utility
{
	public static class FileManager
	{
		public static GraphAsset LoadGraphAsset(string path)
		{
			GraphAsset loadedAsset = AssetDatabase.LoadAssetAtPath<GraphAsset>(path.ToLocalPath());

			if (loadedAsset == null)
			{
				EditorUtility.DisplayDialog("Load Graph", $"The selected file is not of type {nameof(GraphAsset)}!", "Close");
			}
			
			return loadedAsset;
		}

		public static void SaveGraphAssetAs(string path, GraphAsset graphAsset)
		{

		}

		public static void OverwriteGraphAsset(string path)
		{
			
		}

		private static string ToLocalPath(this string absolutePath)
		{
			return absolutePath.Substring(absolutePath.LastIndexOf("Assets/", StringComparison.Ordinal));
		}
	}
}