using System.IO;
using UnityEditor;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.ShortcutCommands
{
	public class ExportCommand : ICommand
	{
		private readonly DialogueAuthorWindow _dialogueAuthorWindow;

		public ExportCommand(DialogueAuthorWindow dialogueAuthorWindow)
		{
			_dialogueAuthorWindow = dialogueAuthorWindow;
		}
		
		public void Execute()
		{
			string path = EditorUtility.SaveFilePanel("Export Dialogue Graph", Application.dataPath, "New Dialogue", "asset");
			if(string.IsNullOrEmpty(path)) return;

			DialogueAsset dialogueAsset;
			
			if (File.Exists(path))
			{
				path = ToRelativePath(path);
				dialogueAsset = (DialogueAsset) AssetDatabase.LoadAssetAtPath(path, typeof(DialogueAsset));

				if (dialogueAsset == null)
				{
					Debug.LogError($"The selected file is not of type {typeof(DialogueAsset)}\nPath: {path}");
					return;
				}

				dialogueAsset.nodes = _dialogueAuthorWindow.activeGraphView.ToDialogueAssetNodes();
				EditorUtility.SetDirty(dialogueAsset);
			}
			else
			{
				path = ToRelativePath(path);
				dialogueAsset = (DialogueAsset) ScriptableObject.CreateInstance(typeof(DialogueAsset));
				dialogueAsset.nodes = _dialogueAuthorWindow.activeGraphView.ToDialogueAssetNodes();
				AssetDatabase.CreateAsset(dialogueAsset, path);
			}
		}

		private string ToRelativePath(string absolutePath)
		{
			return absolutePath.Replace(Application.dataPath, "Assets/");
		}
	}
}
