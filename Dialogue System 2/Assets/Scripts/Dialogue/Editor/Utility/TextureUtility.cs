using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.Utility
{
	public static class TextureUtility
	{
		public static Texture2D indentTexture;
		
		public static Texture2D CreateIndentTexture()
		{
			Texture2D indent = new Texture2D(1, 1);
			indent.SetPixel(0, 0, Color.clear);
			indent.Apply();
			return indent;
		}
	}
}