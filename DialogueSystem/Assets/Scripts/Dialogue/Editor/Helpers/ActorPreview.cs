using UnityEditor;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	[CustomPreview(typeof(GameObject))]
	public class ActorPreview : ObjectPreview
	{
		public override bool HasPreviewGUI() => true;

		public override void OnPreviewGUI(Rect r, GUIStyle background)
		{
			//base.OnPreviewGUI(r, background);
			GUI.Label(r, target.name + " Preview");
		}
	}
}