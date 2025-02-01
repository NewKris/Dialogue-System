using UnityEditor;
using VirtualDeviants.Dialogue.Editor.Graph;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;

namespace VirtualDeviants.Dialogue.Editor.ShortcutCommands
{
	public class SaveCommand : ICommand
	{
		private readonly DialogueAuthorWindow _dialogueAuthorWindow;

		public SaveCommand(DialogueAuthorWindow dialogueAuthorWindow)
		{
			_dialogueAuthorWindow = dialogueAuthorWindow;
		}

		public void Execute()
		{
			_dialogueAuthorWindow.openedGraphAsset.UpdateGraphAssetData(_dialogueAuthorWindow.activeGraphView);
			EditorUtility.SetDirty(_dialogueAuthorWindow.openedGraphAsset);
			_dialogueAuthorWindow.SaveChanges();
		}
	}
}