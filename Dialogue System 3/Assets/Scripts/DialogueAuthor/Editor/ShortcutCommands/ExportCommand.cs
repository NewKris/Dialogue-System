using System.IO;
using UnityEditor;
using UnityEngine;
using VirtualDeviants.DialogueAuthor.Editor.Utility;

namespace VirtualDeviants.DialogueAuthor.Editor.ShortcutCommands {
	public class ExportCommand : ICommand {
		private readonly DialogueAuthorWindow _dialogueAuthorWindow;

		public ExportCommand(DialogueAuthorWindow dialogueAuthorWindow) {
			_dialogueAuthorWindow = dialogueAuthorWindow;
		}
		
		public void Execute() {
			string path = EditorUtility.SaveFilePanel(
				"Export Dialogue Graph", 
				Application.dataPath, 
				"New Dialogue", 
				"asset"
			);

			if (string.IsNullOrEmpty(path)) {
				return;
			}

			if (File.Exists(path)) {
				path = ToRelativePath(path);
				DialogueAsset dialogueAsset = (DialogueAsset)AssetDatabase.LoadAssetAtPath(path, typeof(DialogueAsset));

				if (dialogueAsset == null) {
					Debug.LogError($"The selected file is not of type {typeof(DialogueAsset)}\nPath: {path}");
					return;
				}

				dialogueAsset.nodes = _dialogueAuthorWindow.activeGraphView.ToRuntimeAssetNodes();
				EditorUtility.SetDirty(dialogueAsset);
			}
			else {
				path = ToRelativePath(path);
				DialogueAsset newDialogueAsset = (DialogueAsset)ScriptableObject.CreateInstance(typeof(DialogueAsset));
				newDialogueAsset.nodes = _dialogueAuthorWindow.activeGraphView.ToRuntimeAssetNodes();
				AssetDatabase.CreateAsset(newDialogueAsset, path);
			}
		}

		private string ToRelativePath(string absolutePath) {
			return absolutePath.Replace(Application.dataPath, "Assets/");
		}
	}
}
