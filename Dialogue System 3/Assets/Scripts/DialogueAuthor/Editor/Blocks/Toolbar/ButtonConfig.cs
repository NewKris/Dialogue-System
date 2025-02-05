using VirtualDeviants.DialogueAuthor.Editor.Utility;

namespace VirtualDeviants.DialogueAuthor.Editor.Blocks.Toolbar {
	public readonly struct ButtonConfig {
		public readonly string text;
		public readonly ICommand callback;

		public ButtonConfig(string text, ICommand callback) {
			this.text = text;
			this.callback = callback;
		}
	}
}
