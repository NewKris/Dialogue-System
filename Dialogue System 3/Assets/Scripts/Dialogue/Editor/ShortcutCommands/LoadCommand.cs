using UnityEditor;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.Graph;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.ShortcutCommands
{
	public class LoadCommand : ICommand
	{
		private readonly DialogueAuthorWindow _dialogueAuthorWindow;

		public LoadCommand(DialogueAuthorWindow dialogueAuthorWindow)
		{
			_dialogueAuthorWindow = dialogueAuthorWindow;
		}
		
		public void Execute()
		{
			string path = EditorUtility.OpenFilePanel("Load Graph Asset", Application.dataPath, "asset");
			if(string.IsNullOrEmpty(path)) return;
			
			GraphAsset loadedAsset = FileManager.LoadGraphAsset(path);
			if (loadedAsset != null) _dialogueAuthorWindow.DrawGraphAsset(loadedAsset);
		}
	}
}