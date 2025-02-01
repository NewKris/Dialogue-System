using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Utility
{
	public static class BlockFactory
	{
		public static VisualElement CreateNavigationBar(ButtonConfig[] buttons)
		{
			Toolbar toolbar = new Toolbar();
			toolbar.AddStyleClass(StyleBlackBoard.NavigationBar);

			foreach (ButtonConfig config in buttons)
			{
				ToolbarButton button = VisualElementFactory.CreateToolbarButton(config.text, () => config.callback.Execute());
				button.AddStyleClass(StyleBlackBoard.NavigationBar_TabButton);
				toolbar.Add(button);
			}

			return toolbar;
		}
	}
}