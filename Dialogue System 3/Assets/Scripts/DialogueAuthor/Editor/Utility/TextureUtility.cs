using UnityEngine;

namespace VirtualDeviants.DialogueAuthor.Editor.Utility {
	public static class TextureUtility {
		private static Texture2D IndentTexture;

		public static Texture2D GetIndentTexture() {
			if (IndentTexture == null) {
				IndentTexture = new Texture2D(1, 1);
				IndentTexture.SetPixel(0, 0, Color.clear);
				IndentTexture.Apply();
			}
			
			return IndentTexture;
		}
	}
}