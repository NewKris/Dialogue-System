using UnityEditor;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.ShortcutCommands {
	public class SaveCommand : ICommand {
		private readonly DialogueAuthorWindow _dialogueAuthorWindow;

		public SaveCommand(DialogueAuthorWindow dialogueAuthorWindow) {
			_dialogueAuthorWindow = dialogueAuthorWindow;
		}

		public void Execute() {
			GraphViewToAssetConverter.WriteToGraphAsset(_dialogueAuthorWindow.openedGraphAsset, _dialogueAuthorWindow.activeGraphView);
			EditorUtility.SetDirty(_dialogueAuthorWindow.openedGraphAsset);
			_dialogueAuthorWindow.SaveChanges();
		}
	}
}