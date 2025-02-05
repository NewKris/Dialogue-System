using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Blocks.Toolbar {
	public readonly struct ButtonConfig {
		public readonly string text;
		public readonly ICommand callback;

		public ButtonConfig(string text, ICommand callback) {
			this.text = text;
			this.callback = callback;
		}
	}
}
