using System;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public class DropdownElement : VisualElement
	{
		public DropdownMenu menu;

		public DropdownElement()
		{
			menu = new DropdownMenu();
		}

		public void AppendAction(string menuName, Action<DropdownMenuAction> callback)
		{
			menu.AppendAction(menuName, callback);
		}
		
	}
}